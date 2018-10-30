function aplicarMascaras() {
    $("#telefone").mask("(99) 9999-9999");
    $("#CNPJ").mask("99.999.999/9999-99");
    $("#agencia").mask("999-9");
    $("#conta").mask("99.999-9");
}

//TODO: Apesar de funcional (Cliente/Servidor), melhorar esta parte pois ainda sim dá submit
function checkForm(form) {
    if (form.CategoriaID.value == 5 && form.telefone.value == '') {
        alert('Obrigatório o inserir telefone para estabelecimento Supermercado');
        return false;
    }
}

$("#CNPJ").blur(function () {
    var cpf_cnpj = $(this).val();
    if (isCNPJValid(cpf_cnpj) == false) {
        var test = $("#CNPJ").val();
        if (test != '__.___.___/____-__') {
            alert('CNPJ inválido!');
            $("#CNPJ").val("");
        }
    }
});

$(function () {
    $("#dataCad").datepicker({ dateFormat: 'dd/mm/yy' });
});

function criarTabela() {
    var oTable = $('#tableEstabelecimento').DataTable({
        "ajax": {
            "url": '/Home/Estabelecimentos',
            "type": "get",
            "datatype": "json"
        },
        "columns": [
            { "data": "estabelecimentoID", "autoWidth": true },
            {
                "data": "dataCad",
                "render": function (value) {
                    if (value === null) return "";

                    var pattern = /Date\(([^)]+)\)/;
                    var results = pattern.exec(value);
                    var dt = new Date(parseFloat(results[1]));
                    return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
                }
            },
            { "data": "labelCategoria", "autoWidth": true },
            { "data": "razao", "autoWidth": true },
            { "data": "fantasia", "autoWidth": true },
            { "data": "CNPJ", "autoWidth": true },
            { "data": "email", "autoWidth": true },
            { "data": "endereco", "autoWidth": true },
            { "data": "cidade", "autoWidth": true },
            { "data": "estado", "autoWidth": true },
            { "data": "telefone", "autoWidth": true },
            { "data": "agencia", "autoWidth": true },
            { "data": "conta", "autoWidth": true },
            { "data": "labelStatus", "autoWidth": true },
            {
                "data": "estabelecimentoID", "width": "50px", "render": function (data) {
                    return '<a class="popup btn btn-success" href="/Home/Salvar/' + data + '">Editar</a>';
                }
            },
            {
                "data": "estabelecimentoID", "width": "50px", "render": function (data) {
                    return '<a class="popup btn btn-danger" href="/Home/Deletar/' + data + '">Detalhes</a>';
                }
            }
        ]
    })
    $('.tablecontainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));
    })
    function OpenPopup(pageUrl) {
        var $pageContent = $('<div/>');
        $pageContent.load(pageUrl, function () {
            $('#popupForm', $pageContent).removeData('validator');
            $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse('form');

        });

        $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
            .html($pageContent)
            .dialog({
                draggable: false,
                autoOpen: false,
                resizable: false,        
                model: true,
                title: 'Estabelecimentos',
                height: 615,
                width: 800,
                close: function () {
                    $dialog.dialog('destroy').remove();
                    window.location.href = '/Home/Index';
                }
            })

        $('.popupWindow').on('submit', '#popupForm', function (e) {
            var url = $('#popupForm')[0].action;
            $.ajax({
                type: "POST",
                url: url,
                data: $('#popupForm').serialize(),
                success: function (data) {
                    if (data.status) {
                        $dialog.dialog('close');
                        oTable.ajax.reload();
                        window.location.href = '/Home/Index';
                    }
                }
            })

            e.preventDefault();
        })
        $dialog.dialog('open');
    }
}

function validarCNPJ(field, rules, i, options) {
    var valido = isCNPJValid(field.val()); //implementar a validação
    if (!valido) {
        //internacionalização
        return options.allrules.cnpj.alertText;
    }
}

function isCNPJValid(cnpj) {
    cnpj = cnpj.replace(/[^\d]+/g, '');
    if (cnpj == '') return false;
    if (cnpj.length != 14)
        return false;
    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;
}
