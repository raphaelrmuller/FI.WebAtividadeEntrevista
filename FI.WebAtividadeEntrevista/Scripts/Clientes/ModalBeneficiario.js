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
        '                    <input type="hidden" id="IDBENEF">' +
        '                    <div class="col-md-4">' +
        '                        <div class="form-group">' +
        '                            <label for="Nome">Nome:</label>' +
        '                            <input required="required" type="text" class="form-control" id="NomeBenef" name="Nome" placeholder="Ex.: João" maxlength="50">' +
        '                        </div>' +
        '                    </div>' +
        '                    <div class="col-md-4">' +
        '                        <div class="form-group">' +
        '                            <label for="CPF">CPF:</label>' +
        '                            <input required="required" type="text" class="form-control" id="CPFBenef" name="CPF" placeholder="Ex.: 333.333.333-33" maxlength="14" oninput="formatarCPFInput(this)">' +
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
            AddRowGrid(beneficiario.Nome, beneficiario.CPF.NumeroCPF, index, beneficiario.Id);
        });
    }

    if (open) {
        $('#modal-beneficiarios').modal({
            backdrop: 'static',
            keyboard: false
        });
    }
}

function AddRowGrid(nome, cpf, index, id) {
    var $table = $('#grid');
    if (index === undefined) {
        index = $table.find('tbody tr').length;
    }

    // Se `id` não for fornecido, defina-o como um valor padrão
    id = id || -1;

    let $newRow = $('<tr>');
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].Nome" value="' + nome + '">' + nome));
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].CPF" value="' + cpf + '">' + formatarCPF(cpf)));
    $newRow.append($('<td>').append('<button class="atualizar-row btn btn-primary" style="margin-right: 0px;">Alterar</button> <button class="remove-row btn btn-primary">Excluir</button>'));
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].Id" value="' + id + '">'));

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
    var cpfCliente = limparCPF($('#CPF').val());
    if (cpfCliente === '') {
        cpfExistente = true;
        ModalDialog("Erro", "Digite o CPF do cliente na tela principal antes de cadastrar um beneficiário!");
    }
    cleanCPF = limparCPF(cpf);
    if (cleanCPF === cpfCliente) {
        cpfExistente = true;
        ModalDialog("Erro", "Um beneficiário não pode ter o mesmo CPF do cliente.");
    }
    $('#grid tbody tr').each(function () {
        var cpfTabela = limparCPF($(this).find('input[name*="CPF"]').val());
        if (cpfTabela === cleanCPF) {
            ModalDialog("Erro", "Este CPF já foi cadastrado.");
            cpfExistente = true;
            return false;
        }
    });

    return cpfExistente;
}
