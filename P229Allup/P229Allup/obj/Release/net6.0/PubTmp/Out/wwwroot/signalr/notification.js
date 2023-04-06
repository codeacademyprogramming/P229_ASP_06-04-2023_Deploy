let connection = new signalR.HubConnectionBuilder().withUrl('/notificationHub').build();
connection.start().then(error => {
    console.log(error)
});

connection.on("changedorder", function (message) {
    console.log(message)

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    toastr["success"](message)
});

connection.on("checkOrder", function (message) {
    console.log(message)

    toastr["success"](message)
});