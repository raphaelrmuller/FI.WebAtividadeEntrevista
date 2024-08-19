function formatarCPFInput(input) {
    var cpf = input.value.replace(/\D/g, '');
    if (cpf.length > 3 && cpf.length <= 6) {
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
    } else if (cpf.length > 6 && cpf.length <= 9) {
        cpf = cpf.replace(/(\d{3})(\d{3})(\d)/, '$1.$2.$3');
    } else if (cpf.length > 9) {
        cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d)/, '$1.$2.$3-$4');
    }
    input.value = cpf;
}
function formatarCPF(cpf) {
    // Remove caracteres não numéricos
    cpf = cpf.replace(/\D/g, '');

    // Verifica se a string tem exatamente 11 dígitos
    if (cpf.length !== 11) {
        throw new Error('CPF deve conter exatamente 11 dígitos');
    }

    // Formata o CPF
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}
function limparCPF(cpf) {
    return cpf.replace(/\D/g, '');
}
function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var textoFormatado = texto.replace(/\n/g, '<br>');
    var texto = '<div id="' + random + '" class="modal fade">' +
        '        <div class="modal-dialog">' +
        '            <div class="modal-content">' +
        '                <div class="modal-header">' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '                    <h4 class="modal-title">' + titulo + '</h4>' +
        '                </div>' +
        '                <div class="modal-body">' +
        '                    <p>' + textoFormatado + '</p>' +
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