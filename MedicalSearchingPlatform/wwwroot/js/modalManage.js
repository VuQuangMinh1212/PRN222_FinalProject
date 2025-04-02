$(document).ready(function () {
    jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPost = (event, form) => {
        event.preventDefault();
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#modal-message').html(res.message).removeClass("text-danger").addClass("text-success");
                        $('#form-modal form')[0].reset();
                        setTimeout(() => {
                            $('#form-modal').modal('hide');
                            $('#modal-message').html("");
                            location.href = res.redirect;
                        }, 2000)
                    } else {
                        $('#modal-message').html(res.message).removeClass("test-success").addClass("text-danger");
                        $('#form-modal form')[0].reset();
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
        return false;
    }

    jQueryModalDelete = (event, form) => {
        event.preventDefault();
        if (confirm('Are you sure to delete this record ?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid) {
                            $('#modal-message').html(res.message).removeClass("text-danger").addClass("text-success");
                            setTimeout(() => {
                                $('#form-modal').modal('hide');
                                $('#modal-message').html("");
                                location.href = res.redirect;
                            }, 2000)
                        } else {
                            $('#modal-message').html(res.message).removeClass("test-success").addClass("text-danger");
                        }
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }
        return false;
    }

    submitDeleteForm = (scheduleId) => {
        // Set the schedule ID for the delete form
        $('#deleteScheduleId').val(scheduleId);
        // Submit the delete form
        $('#deleteForm').submit();
    }
});