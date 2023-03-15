const IsEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var IsPhoneNo = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
function isValidate() {
    var email = $("#Email").val();
    var password = $("#login-password").val();
    if (email.trim() == "") {
        $("#errorMsg").html('Please enter the user name.');
        $("#errorMsg").show();
        return false;
    }
    else if (password == null || password.trim() == "") {
        $("#errorMsg").html('Please fill the password');
        $("#errorMsg").show();
        return false;
    }
    else {
        $("#errorMsg").html('');
        $(".loader").show();
    }
}

function isValidateForgotForm() {
    var email = $("#EmailAddress").val();
    if (IsEmail.test(email.toLowerCase()) == false) {
        $("#errorMsg").html('Please enter the valid email address.');
        $("#errorMsg").show();
        return false;
    }
    else {
        $("#errorMsg").html('');
        $(".loader").show();
    }
}

function isValidateResetForm() {
    var oldPassword = $("#oldPassword").val();
    var newPassword = $("#newPassword").val();
    var confirmPassword = $("#confirmPassword").val();
    if (oldPassword == null || oldPassword.trim() == "") {
        $("#errorMsg").html('Please enter the old password.');
        $("#errorMsg").show();
        return false;
    }
    else if (newPassword == null || newPassword.trim() == "") {
        $("#errorMsg").html('Please enter the new password.');
        $("#errorMsg").show();
        return false;
    }
    else if (confirmPassword == null || confirmPassword.trim() == "") {
        $("#errorMsg").html('Please enter the confirm password.');
        $("#errorMsg").show();
        return false;
    }
    else if (confirmPassword.trim() != newPassword.trim()) {
        $("#errorMsg").html('Please specify the same password as above.');
        $("#errorMsg").show();
        return false;
    }
    else {
        $("#errorMsg").html('');
        $(".loader").show();
        var pwdModel = {};
        pwdModel.OldPassword = oldPassword;
        pwdModel.NewPassword = newPassword;
        pwdModel.ConfirmPassword = confirmPassword;
        $.ajax({
            type: "POST",
            url: '../Account/ResetPassword',
            data: '{model: ' + JSON.stringify(pwdModel) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (responce) {
                if (responce.isUpdated) {
                    $(".loader").hide();;
                    $("#oldPassword").val('');
                    $("#newPassword").val('');
                    $("#confirmPassword").val('');
                    toastr.success(responce.Message);
                }
                else if (responce.Message == "login at another client") {
                    toastr.error(responce.Message);
                    window.location.href = '../account/login?Message=' + responce.Message;
                }
                else {
                    $(".loader").hide();;
                    toastr.error(responce.Message);
                }
            }
        });
    }
}

function isRegisterUserForm() {
    var userName = $("#userName").val();
    var fullName = $("#fullName").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var phoneNo = $("#phoneNo").val();
    if (userName == null || userName.trim() == "") {
        $("#errorMsg").html('User name field is required.');
        $("#errorMsg").show();
        return false;
    }
    else if (fullName == null || fullName.trim() == "") {
        $("#errorMsg").html('Full name field is required.');
        $("#errorMsg").show();
        return false;
    }
    else if (email == null || email.trim() == "") {
        $("#errorMsg").html('Email field is required.');
        $("#errorMsg").show();
        return false;
    }
    else if (IsEmail.test(email.toLowerCase()) == false) {
        $("#errorMsg").html('Please enter the valid email address.');
        $("#errorMsg").show();
        return false;
    }
    else if (password == null || password.trim() == "") {
        $("#errorMsg").html('Password field is required.');
        $("#errorMsg").show();
        return false;
    }
    else if (phoneNo == null || phoneNo.trim() == "") {
        $("#errorMsg").html('Phone No field is required.');
        $("#errorMsg").show();
        return false;
    }
    else if (IsPhoneNo.test(phoneNo) == false) {
        $("#errorMsg").html('Please enter a valid phone number');
        $("#errorMsg").show();
        return false;
    }
    else {
        $("#errorMsg").html('');
        $(".loader").show();
    }
}