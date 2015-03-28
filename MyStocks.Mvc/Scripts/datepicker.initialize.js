
$(document).ready(function () {
    // use the language provided from server...
    var lang = $("html").attr("lang");

    // mapping from cultures supported by the application
    //  to cultures supported by datepicker...
    //  (with just two languages this is the easiest approach...)
    if (lang == "de-DE")
        $.datepicker.setDefaults($.datepicker.regional["de"]);
    else
        $.datepicker.setDefaults($.datepicker.regional[""]); // en-US is default

    // turn textboxes into datepickers
    $("input[type='date']").datepicker();
});
