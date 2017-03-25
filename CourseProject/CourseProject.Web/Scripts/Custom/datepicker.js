$(function () {
    $(".datefield").datepicker({
        dateFormat: 'dd/MM/yy',
        changeMonth: true,
        changeYear: true,
        minDate: new Date(1400, 1, 1),
        maxDate: new Date(3000, 1, 1),
        defaultDate: new Date(2017, 3, 1) 
    });
});