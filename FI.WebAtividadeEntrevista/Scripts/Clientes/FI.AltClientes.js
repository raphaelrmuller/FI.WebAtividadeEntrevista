$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        var cpfField = $('#formCadastro #CPF');
        cpfField.val(obj.CPF.NumeroCPF);
        formatarCPF(cpfField[0]);
    }

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
            error: function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                ModalDialog("Sucesso!", r);
                $("#formCadastro")[0].reset();
                window.location.href = urlRetorno;
            }
        });
    });

    $('#add-row').on('click', function () {
        var nome = $('#NomeBenef').val();
        var cpf = $('#CPFBenef').val();

        if (nome && cpf) {
            if (validarCPFExistente(cpf)) {
                ModalDialog("Erro", "Este CPF já foi cadastrado.");
            } else {
                AddRowGrid(nome, cpf);
                $('#NomeBenef').val('');  // Limpa o campo de nome
                $('#CPFBenef').val('');   // Limpa o campo de CPF
            }
        } else {
            ModalDialog("Erro", "Por favor, preencha todos os campos.");
        }
    });

    // Eventos delegados para as ações de alterar e excluir
    $('#grid').on('click', '.remove-row', function () {
        var $row = $(this).closest('tr');
        $row.remove();
        reorganizarIndices();
    });

    $('#grid').on('click', '.atualizar-row', function () {
        var $row = $(this).closest('tr');
        $('#NomeBenef').val($row.find('input[name*="Nome"]').val());
        $('#CPFBenef').val($row.find('input[name*="CPF"]').val());
        $row.remove();
        reorganizarIndices();
    });
});

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">' +
        '        <div class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '                    <h4 class="modal-title">' + titulo + '</h4>' +
        '                </div>' +
        '                <div class="modal-body">' +
        '                    <p>' + texto + '</p>' +
        '                </div>' +
        '                <div class="modal-footer">' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
        '                </div>' +
        '            </div>' +
        '  </div>' +
        '</div>';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function ModalDialogBeneficiarios(beneficiarios, open) {
    var texto = '<div id="modal-beneficiarios" class="modal fade">' +
        '    <div class="modal-dialog">' +
        '        <div class="modal-content">' +
        '            <div class="modal-header">' +
        '                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '                <h4 class="modal-title">Cadastro Beneficiário</h4>' +
        '            </div>' +
        '            <div class="modal-body">' +
        '                <div class="row">' +
        '                    <div class="col-md-4">' +
        '                        <div class="form-group">' +
        '                            <label for="Nome">Nome:</label>' +
        '                            <input required="required" type="text" class="form-control" id="NomeBenef" name="Nome" placeholder="Ex.: João" maxlength="50">' +
        '                        </div>' +
        '                    </div>' +
        '                    <div class="col-md-4">' +
        '                        <div class="form-group">' +
        '                            <label for="CPF">CPF:</label>' +
        '                            <input required="required" type="text" class="form-control" id="CPFBenef" name="CPF" placeholder="Ex.: 333.333.333-33" maxlength="30">' +
        '                        </div>' +
        '                    </div>' +
        '                    <div class="col-md-4">' +
        '                        <div class="form-group">' +
        '                            <button id="add-row" type="button" class="btn btn-sm btn-success" style="margin-top: 24px;">Incluir</button>' +
        '                        </div>' +
        '                    </div>' +
        '                </div>' +
        '                <div class="row">' +
        '                    <div class="col-md-12">' +
        '                        <table id="grid" class="table table-lg">' +
        '                            <thead>' +
        '                                <tr>' +
        '                                    <th>Nome</th>' +
        '                                    <th>CPF</th>' +
        '                                    <th>Ações</th>' +
        '                                </tr>' +
        '                            </thead>' +
        '                            <tbody>' +
        '                            </tbody>' +
        '                        </table>' +
        '                    </div>' +
        '                </div>' +
        '            </div>' +
        '            <div class="modal-footer">' +
        '                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '</div>';

    $('body').append(texto);

    // Preenche a tabela com os beneficiários já existentes
    if (beneficiarios && beneficiarios.length > 0) {
        beneficiarios.forEach(function (beneficiario, index) {
            AddRowGrid(beneficiario.Nome, beneficiario.CPF.NumeroCPF, index);
        });
    }

    if (open) {
        $('#modal-beneficiarios').modal({
            backdrop: 'static',
            keyboard: false
        });
    }
}

function AddRowGrid(nome, cpf, index) {
    var $table = $('#grid');
    if (index === undefined) {
        index = $table.find('tbody tr').length;
    }

    let $newRow = $('<tr>');
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].Nome" value="' + nome + '">' + nome));
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].CPF" value="' + cpf + '">' + cpf));
    $newRow.append($('<td>').append('<button class="atualizar-row btn btn-primary" style="margin-right: 0px;">Alterar</button>'));
    $newRow.append($('<td>').append('<button class="remove-row btn btn-primary">Excluir</button>'));

    $table.find('tbody').append($newRow);
}

function reorganizarIndices() {
    var $table = $('#grid');
    $table.find('tbody tr').each(function (i, row) {
        var $row = $(row);
        var $inputs = $row.find('input');
        $inputs.each(function (j, input) {
            var name = $(input).attr('name');
            name = name.replace(/\[\d+\]/, '[' + i + ']');
            $(input).attr('name', name);
        });
    });
}

function validarCPFExistente(cpf) {
    var cpfExistente = false;

    $('#grid tbody tr').each(function () {
        var cpfTabela = $(this).find('input[name*="CPF"]').val();
        if (cpfTabela === cpf) {
            cpfExistente = true;
            return false; // Interrompe o loop
        }
    });

    return cpfExistente;
}
