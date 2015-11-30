var idTime = 1;
function AddTime(id) {
    var index = 0;

    index = id.substring(6);
    var element = $('#Dates_0Times_0Id').clone();

    var m = $("span#divcust" + index + " select:last-of-type").attr("id")
    if (m != null && m.length > 0) {
        idTime = m.substring(13);
        idTime++;

    }
        var container = $('#divcust' + index);

     //new hidden
        element.attr('id', "Dates_" + index + "Times_" + idTime+"__Id");
        element.attr('name', "Dates[" + index + "].Times[" + idTime + "].Id");
        container.append(element);

    //new time
        element = $('#Dates_0Times_0').clone();
        element.attr('id', "Dates_" + index + "Times_" + idTime);
        element.attr('name', "Dates[" + index + "].Times[" + idTime + "].Time");
        container.append(element); 

    //button del
        var button = "<button id='btnDel-" + index + "_" + idTime + "' type='button' onclick='DelTime(this.id);'  class='btn btn-default btn-xs'>  <spann class='glyphicon glyphicon-remove' aria-hidden='true' /></ button>";
            container.append(button);
            window.idTime++;
};


function DelTime(id) {
    var indexDel = id.substring(id.indexOf("-") + 1, id.indexOf("_") );
    var indexTime = id.substring(id.indexOf("_") + 1);
    $("#Dates_" + indexDel + "Times_" + indexTime).remove();
    $('#'+ id).remove();
};


        