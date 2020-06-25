

    function message_Success(strMess) {
        $('.notifyjs-wrapper').remove();
    $.notify(
        strMess,
        "success"
    );
}

        function scrollToSection(id) {
            var targetElement = $('#' + id);
            $([document.documentElement, document.body]).animate({
        scrollTop: targetElement.offset().top
}, 900);
}

        function checkValidTxt(idTxt) {
            var strLength = "Nhập thiếu .Thông tin phải bằng hoặc lớn hơn 6 kí tự";
    var strTxt = $('#' + idTxt).val();

            if (!strTxt) {
        message_Error(idTxt, STR_WARNING_EMPTY);
    return false;
}
            else if (strTxt.length < 6) {
        message_Error(idTxt, STR_WARNING_EMPTY);
    return false;
}
return true;
}

//check caplock
var flagCheckCapLock = true;
var strCapLock = "Cap Lock đang bật.Hãy tắt để nhập thông tin";

        function checkCapLock(idTxt) {
            var txt = document.getElementById(idTxt);
            txt.addEventListener("keyup", function (event) {
                if (event.getModifierState("CapsLock")) {
        flagCheckCapLock = false;
    message_Error(idTxt, strCapLock);

                } else {
        flagCheckCapLock = true;
    $('.notifyjs-wrapper').remove();
}
});
}
        function message_Error(id, strMess) {
            if (id === '') {
        //setTimeout($('.notifyjs-wrapper').remove(), 2000);
        $.notify(
            strMess,
            { position: "top-center" }
        );
    }
            else {
        //setTimeout($('.notifyjs-wrapper').remove(), 2000);
        $("#" + id).notify(
            strMess,
            { position: "top center" }
        );
    }
}