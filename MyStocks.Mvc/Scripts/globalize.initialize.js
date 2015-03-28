
$(document).ready(function () {
    // use the language provided from server...
    var lang = $("html").attr("lang");

    // default value
    if (typeof(lang) != "string")
        lang = "en-US";

    // and go!
    if (typeof Globalize != 'undefined')
        Globalize.culture(lang);
});
