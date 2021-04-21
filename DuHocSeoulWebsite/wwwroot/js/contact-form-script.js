$("#contactForm").validator().on("submit", function (event) {
    if (event.isDefaultPrevented()) {
        // handle the invalid form...
        formError();
        submitMSG(false, "Vui lòng điền đầy đủ các thông tin yêu cầu");
    } else {
        // everything looks good!
        event.preventDefault();
        sendRequest();
    }
});

var processingImageUrl = '<img src="../img/ic-loading.gif" width="64" height="64" />';
var requestUrl = "/Home/SendRequest";
function sendRequest() {

    var name = $("#name").val();
    var email = $("#email").val();
    var phone = $("#phone").val();
    var message = $("#message").val();

    var requestData = JSON.stringify({ "name": name, "email": email, "phone": phone, "content": message });
    $("#contactForm").block({ message: processingImageUrl });
    $.ajax({
        url: requestUrl,
        type: "POST",
        headers: { 'Content-Type': 'application/json' },
        dataType: "json",
        data: requestData,
        success: function (data) {
            $("#contactForm").unblock();

            if (data.hasError) {
                formError();
                submitMSG(false, data.errorMessage);
            } else {
                formSuccess("Đã gửi yêu cầu thành công. Chúng tôi sẽ liên lạc với bạn trong thời gian sớm nhất!");
            }
        },
        error: function () {
        }
    });
}

function formSuccess(msg){
    $("#contactForm")[0].reset();
    submitMSG(true, msg);
}

function formError(){
    $("#contactForm").removeClass().addClass('shake animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function(){
        $(this).removeClass();
    });
}

function submitMSG(valid, msg) {
    var msgClasses = "";
    if(valid){
        msgClasses = "h5 text-center tada animated text-success p-3";
    } else {
        msgClasses = "h5 text-center text-danger";
    }
    $("#msgSubmit").removeClass().addClass(msgClasses).text(msg);
}