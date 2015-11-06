var idTime = 1;

function AddTime(id) {
    var index = 0;
    index = id.substring(6);
        var element = $('#Dates_0__Times_0_').clone();
        var container = $('#divcust' + index);
            element.attr('id', "Times" + idTime);
            element.attr('name', "Times" + idTime);
            container.append(element);
            var button = "<input id='btnDel" + idTime + "' type='button' onclick='DelTime(this.id);' value='Delete'  />";
            container.append(button);

        idTime++;
};


function DelTime(id) {
    var indexDel = 0;
    indexDel = id.substring(6);
    $('#Times' + indexDel).remove();
    $('#' + id).remove();
};

        