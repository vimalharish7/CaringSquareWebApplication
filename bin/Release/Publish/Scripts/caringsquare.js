$(document).ready(function () {
    $('#tableExample').DataTable();
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