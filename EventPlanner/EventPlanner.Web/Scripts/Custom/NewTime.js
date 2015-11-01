var idTime = 0;
function AddTime(id) {
    var index = 0;
        index = id.substring(6);
        var html =
            "<select name='TimeSlots_" + idTime + "' class='form-control'  id='TimeSlots" + idTime + "' > <option value='0' selected='selected'>00:00</option> <option value='1'>01:00</option><option value='2'>02:00</option><option value='3'>03:00</option><option value='4'>04:00</option><option value='5'>05:00</option><option value='6'>06:00</option>" +
                "<option value='7'>07:00</option><option value='8'>08:00</option><option value='9'>09:00</option><option value='10'>10:00</option><option value='11'>11:00</option><option value='12'>12:00</option><option value='13'>13:00</option><option value='14'>14:00</option>" +
                "<option value='15'>15:00</option><option value='16'>16:00</option><option value='17'>17:00</option><option value='18'>18:00</option><option value='19'>19:00</option><option value='20'>20:00</option><option value='21'>21:00</option><option value='22'>22:00</option><option value='23'>23:00</option></select>" +
                "<input id='btnDel" + idTime + "' type='button' value='Delete' onclick='DelTime(this.id);'  class='glyphicon glyphicon-remove' aria-hidden='true' />";
        $('#divcust' + index).append(html);
       idTime++;

};

function DelTime(id) {
    var indexDel = 0;
    indexDel = id.substring(6);
    $('#TimeSlots' + indexDel).remove();
    $('#'+id).remove();
};
