var idTime = 1;
function AddTime(id) {
    var index = 0;
    index = id.substring(6);
        var element = $('#Dates_0__Times_0_').clone();
        var container = $('#divcust' + index);
        element.attr('id', "Dates_" + index + "Times_" + idTime);
            element.attr('name', "Dates[" + index + "].Times[" + idTime + "]");
            container.append(element);
            var button = "<input id='btnDel-" + index + "_" + idTime + "' type='button' onclick='DelTime(this.id);' value='Delete'  />";
            container.append(button);
            window.idTime++;
};


function DelTime(id) {
    var indexDel = id.substring(id.indexOf("-") + 1, id.indexOf("_") );
    var indexTime = id.substring(id.indexOf("_") + 1);
    $("#Dates_" + indexDel + "Times_" + indexTime).remove();
    $('#'+ id).remove();
};

        