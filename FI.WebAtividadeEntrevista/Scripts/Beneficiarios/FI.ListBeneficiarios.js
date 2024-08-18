function ModalDialogBeneficiarios() {
    var texto = '<div id="modal-beneficiarios" class="modal fade">                                                                                                                  ' +
        '    <div class="modal-dialog">                                                                                                                                     ' +
        '        <div class="modal-content">                                                                                                                                ' +
        '            <div class="modal-header">                                                                                                                             ' +
        '                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>                                                             ' +
        '                <h4 class="modal-title">Cadastro Beneficiário</h4>                                                                                                 ' +
        '            </div>                                                                                                                                                 ' +
        '            <div class="modal-body">                                                                                                                               ' +
        '                <div class="row">                                                                                                                                  ' +
        '                    <div class="col-md-4">                                                                                                                         ' +
        '                        <div class="form-group">                                                                                                                   ' +
        '                            <label for="Nome">Nome:</label>                                                                                                        ' +
        '                            <input required="required" type="text" class="form-control" id="NomeBenef" name="Nome" placeholder="Ex.: João" maxlength="50">         ' +
        '                        </div>                                                                                                                                     ' +
        '                    </div>                                                                                                                                         ' +
        '                    <div class="col-md-4">                                                                                                                         ' +
        '                        <div class="form-group">                                                                                                                   ' +
        '                            <label for="CPF">CPF:</label>                                                                                                          ' +
        '                            <input required="required" type="text" class="form-control" id="CPFBenef" name="CPF" placeholder="Ex.: 333.333.333-33" maxlength="30"> ' +
        '                        </div>                                                                                                                                     ' +
        '                    </div>                                                                                                                                         ' +
        '                    <div class="col-md-4">                                                                                                                         ' +
        '                        <div class="form-group">                                                                                                                   ' +
        '                            <button id="add-row" type="button" class="btn btn-sm btn-success" style="margin-top: 24px;" onclick="AddRowGrid()">Incluir</button>    ' +
        '                        </div>                                                                                                                                     ' +
        '                    </div>                                                                                                                                         ' +
        '                </div>                                                                                                                                             ' +
        '                <div class="row">                                                                                                                                  ' +
        '                    <div class="col-md-12">                                                                                                                        ' +
        '                        <table id="grid" class="table table-lg">                                                                                                   ' +
        '                            <thead>                                                                                                                                ' +
        '                                <tr>                                                                                                                               ' +
        '                                    <th>Nome</th>                                                                                                                  ' +
        '                                    <th>CPF</th>                                                                                                                   ' +
        '                                </tr>                                                                                                                              ' +
        '                            </thead>                                                                                                                               ' +
        '                            <tbody>                                                                                                                                ' +
        '                            </tbody>                                                                                                                               ' +
        '                        </table>                                                                                                                                   ' +
        '                    </div>                                                                                                                                         ' +
        '                </div>                                                                                                                                             ' +
        '            </div>                                                                                                                                                 ' +
        '            <div class="modal-footer">                                                                                                                             ' +
        '                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>                                                                 ' +
        '            </div>                                                                                                                                                 ' +
        '        </div><!-- /.modal-content -->                                                                                                                             ' +
        '    </div><!-- /.modal-dialog -->                                                                                                                                  ' +
        '</div> <!-- /.modal -->                                                                                                                                            ';
    $('body').append(texto);
    $('#modal-beneficiarios').modal('show');
}

function AddRowGrid() {
    var $table = $('#grid');
    var index = $table.find('tbody tr').length;
    let nome = $('#NomeBenef').val();
    let cpf = $('#CPFBenef').val();


    let $newRow = $('<tr>');
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].Nome" value="' + nome + '">' + nome));
    $newRow.append($('<td>').append('<input type="hidden" name="Beneficiarios[' + index + '].CPF" value="' + cpf + '">' + cpf));
    $newRow.append($('<td>').append('<button class="remove-row btn btn-danger">- Remove</button>'));
    $table.find('tbody').append($newRow);
    index++;

    $table.on('click', '.remove-row', function () {
        var $row = $(this).closest('tr');
        var rowIndex = $row.index();

        $row.remove();

        // reorganize the index for the remaining rows
        $table.find('tbody tr').each(function (i, row) {
            var $row = $(row);
            var $inputs = $row.find('input, select');
            $inputs.each(function (j, input) {
                var name = $(input).attr('name');
                name = name.replace(/\[\d+\]/, '[' + i + ']');
                $(input).attr('name', name);
            });
        });

        index--;
    });

}




