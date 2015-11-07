var idTime = 1;
function AddTime(id) {
    var index = 0;
    index = id.substring(6);
        var element = $('#Dates_0__Times_0_').clone();
        var container = $('#divcust' + index);
            element.attr('id', "Dates_"+index+"__Times_" + idTime +"_");
            element.attr('name', "Dates_" + index + "__Times_" + idTime + "_");
            container.append(element);
            var button = "<input id='btnDel" + idTime + "' type='button' onclick='DelTime(this.id);' value='Delete'  />";
            container.append(button);
            window.idTime++;
};


function DelTime(id) {
    var indexDel = 0;
    indexDel = id.substring(6);
    $("#Dates_"+indexx+"__Times_" + indexDel+"_").remove();
    $('#' + id).remove();
};

        