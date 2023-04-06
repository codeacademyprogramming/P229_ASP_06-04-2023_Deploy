$(document).ready(function () {
    $(document).on('click', '.deleteImage', function (e) {
        e.preventDefault();

        let url = $(this).attr('href');

        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('.imageContainer').html(data)
            })
    })

    //let isMainVal = $('#IsMain').attr('data-val');

    //console.log(isMainVal);

    //if (isMainVal == 'true') {
    //    $('#IsMain').attr('checked', 'checked');
    //}

    let path = window.location.pathname.split('/');
    let action = path[3]
    let controller = path[2]
    console.log(action + ' ' + controller)
    if (action?.toLowerCase() == 'create' && controller?.toLowerCase() == 'category') {
        let isMain = $('#IsMain').is(':checked');

        if (isMain) {
            $('.fileInput').removeClass('d-none');
            $('.parentInput').addClass('d-none');
        } else {
            $('.fileInput').addClass('d-none');
            $('.parentInput').removeClass('d-none');
        }
    }

    $('#IsMain').click(function () {
        let isMain = $(this).is(':checked');

        if (isMain) {
            $('.fileInput').removeClass('d-none');
            $('.parentInput').addClass('d-none');
        } else {
            $('.fileInput').addClass('d-none');
            $('.parentInput').removeClass('d-none');
        }
    })

    $(document).on('click', '.deleteBtn', function (e) {
        e.preventDefault();
        let url = $(this).attr('href');
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(url)
                    .then(res => res.text())
                    .then(data => {
                        $('.indexContainer').html(data)
                    })

                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            }
        })
    })
})