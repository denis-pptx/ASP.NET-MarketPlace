function showToast(type, message) {
    var toast = $('<div class="toast" role="alert" aria-live="assertive" aria-atomic="true">');
    var header = $('<div class="toast-header">');
    var icon = $('<i class="fas fa-exclamation-circle me-2"></i>');
    var title = $('<strong class="me-auto">');
    var body = $('<div class="toast-body">');
    var dismiss = $('<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>');
    switch (type) {
        case 'success':
            /*toast.addClass('bg-success text-white');*/
            title.text('Успешно');
            break;
        case 'error':
            toast.addClass('bg-danger text-white');
            title.text('Ошибка');
            break;
    }
    body.text(message);
    header.append(icon).append(title).append(dismiss);
    toast.append(header).append(body);
    $('.toast').toast('dispose'); 
    $('.toast-container').append(toast);
    $('.toast').toast('show');

    setTimeout(function () {
        toast.toast('hide');
        setTimeout(function () {
            toast.remove();
        }, 500); 
    }, 2000);
}