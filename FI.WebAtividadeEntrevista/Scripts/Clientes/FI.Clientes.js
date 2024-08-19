$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        // Extrai a lista de beneficiários da tabela
        var beneficiarios = [];
        $('#grid tbody tr').each(function () {
            var nome = $(this).find('input[name*="Nome"]').val();
            var cpf = $(this).find('input[name*="CPF"]').val();

            beneficiarios.push({
                Nome: nome,
                "CPF.NumeroCPF": cpf
            });
        });
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF.NumeroCPF": $(this).find("#CPF").val(),
                "Beneficiarios": beneficiarios
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                }
        });
    });

    $('#add-row').on('click', function () {
        var nome = $('#NomeBenef').val();
        var cpf = $('#CPFBenef').val();

        if (nome && cpf) {
            if (validarCPFExistente(cpf) === false) {
                AddRowGrid(nome, cpf, undefined, $('#IDBENEF').val());
                $('#NomeBenef').val('');  // Limpa o campo de nome
                $('#CPFBenef').val('');   // Limpa o campo de CPF
                $('#IDBENEF').val('');   // Limpa o campo de CPF
            }
        } else {
            ModalDialog("Erro", "Por favor, preencha todos os campos.");
        }
    });


    $('#grid').on('click', '.remove-row', function () {
        var $row = $(this).closest('tr');
        $row.remove();
        reorganizarIndices();
    });

    $('#grid').on('click', '.atualizar-row', function () {
        var $row = $(this).closest('tr');
        var cpfb = formatarCPF($row.find('input[name*="CPF"]').val());

        $('#NomeBenef').val($row.find('input[name*="Nome"]').val());
        $('#CPFBenef').val(cpfb).val();
        $('#IDBENEF').val($row.find('input[name*="Id"]').val());
        $row.remove();
        reorganizarIndices();
    });
});