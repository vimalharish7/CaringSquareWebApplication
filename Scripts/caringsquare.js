

$(document).ready(function () {
    $('#tableExample tbody tr:even').css('background-color', '#94D16A');
});

$(document).ready(function () {
    var field = document.getElementById("dateValue");
    var date = new Date();
        field.value = date.getFullYear().toString() + '-' + (date.getMonth() + 1).toString().padStart(2, 0) +
        '-' + date.getDate().toString().padStart(2, 0);
});


$(document).ready(function () {

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $('#datepicker').datepicker({
        format: "mm/dd/yyyy",
        todayHighlight: true,
        startDate: today,
        endDate: end,
        autoclose: true
    });

    $('#datepicker').datepicker('setDate', today);
});