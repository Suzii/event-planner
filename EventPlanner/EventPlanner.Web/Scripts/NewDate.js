document.writeln("<script type='text/javascript' src='/Scripts/Pikaday.js'></script>");

itemIndex = 0;


$(function () {
    $("#btnAdd" + window.itemIndex).click(function (e) {
        console.log(itemIndex + " container");
        e.preventDefault();
        var newItem = $("<select name='TimeSlots" + window.itemIndex + "'> <option value='0' selected='selected'>00:00</option> <option value='1'>01:00</option><option value='2'>02:00</option><option value='3'>03:00</option><option value='4'>04:00</option><option value='5'>05:00</option><option value='6'>06:00</option>" +
            "<option value='7'>07:00</option><option value='8'>08:00</option><option value='9'>09:00</option><option value='10'>10:00</option><option value='11'>11:00</option><option value='12'>12:00</option><option value='13'>13:00</option><option value='14'>14:00</option>" +
            "<option value='15'>15:00</option><option value='16'>16:00</option><option value='17'>17:00</option><option value='18'>18:00</option><option value='19'>19:00</option><option value='20'>20:00</option><option value='21'>21:00</option><option value='22'>22:00</option><option value='23'>23:00</option></select>");
        $("#container" + window.itemIndex).append(newItem);
    });
});

$(function () {
    $("#btnAddDate").click(function (e) {
        window.itemIndex++;
        e.preventDefault();
        console.log(window.itemIndex + " uvod");
        var newItem = $("<br /><label>Date</label><input id='DatePickerModule" + window.itemIndex + "' name='TimeSlots" + window.itemIndex + "' type='text' value></TextBox><label>Times</label><span id='container" + window.itemIndex + "'><select name='TimeSlots" + window.itemIndex + "'>" +
            "<option value='0' selected>00:00</option> <option value='1'>01:00</option><option value='2'>02:00</option><option value='3'>03:00</option><option value='4'>04:00</option><option value='5'>05:00</option><option value='6'>06:00</option>" +
            "<option value='7'>07:00</option><option value='8'>08:00</option><option value='9'>09:00</option><option value='10'>10:00</option><option value='11'>11:00</option><option value='12'>12:00</option><option value='13'>13:00</option><option value='14'>14:00</option>" +
            "<option value='15'>15:00</option><option value='16'>16:00</option><option value='17'>17:00</option><option value='18'>18:00</option><option value='19'>19:00</option><option value='20'>20:00</option><option value='21'>21:00</option><option value='22'>22:00</option><option value='23'>23:00</option></select>" +
            "</span> <input type='button' id='btnAdd" + window.itemIndex + "' value='Add another time'/>");
        $("#containerDates").append(newItem);


        var picker = new Pikaday({
            field: document.getElementById('DatePickerModule'+ window.itemIndex),
            firstDay: 1,
            minDate: new Date(),
            maxDate: new Date('2020-01-01'),
            numberOfMonths: 1
        });

       
    });

});






  