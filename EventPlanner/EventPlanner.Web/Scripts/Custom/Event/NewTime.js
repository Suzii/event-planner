var idTime = 1;
function AddTime(id) {
    var index = 0;

    index = id.substring(6);
    var m = $("span#divcust" + index + " select:last-of-type").attr("id");
    if (m != null && m.length > 0) {
        idTime = m.substring(13);
        idTime++;

    }
    var container = $('#divcust' + index);

    //new hidden
    var element = $('#Dates_0Times_0Id').clone();
    element.attr('id', "Dates_" + index + "Times_" + idTime + "__Id");
    element.attr('name', "Dates[" + index + "].Times[" + idTime + "].Id");
    element.attr('value', '');
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
    var indexDel = id.substring(id.indexOf("-") + 1, id.indexOf("_"));
    var indexTime = id.substring(id.indexOf("_") + 1);

    //checked if it has siblings
    var m = $("select#Dates_" + indexDel + "Times_" + indexTime).nextAll("select");

    if (m.length !== 0) {

        var indexTimeMinus = indexTime;
        var position = indexTime;
        position++;
        for (var i = 0; i <= m.length; i++) {
            //change id and name
            console.debug('indexDel=' + indexDel + '; position=' + position);
            var n = $("select#Dates_" + indexDel + "Times_" + position);
            n.attr("id", "Dates_" + indexDel + "Times_" + indexTimeMinus);
            n.attr('name', "Dates[" + indexDel + "].Times[" + indexTimeMinus + "].Time");

            //change del button
            var b = $("select#Dates_" + indexDel + "Times_" + indexTimeMinus).next("button");
            b.attr("id", "btnDel-" + indexDel + "_" + indexTimeMinus);

            //change hidden
            var h = $("#Dates_" + indexDel + "Times_" + position + "__Id");
            h.attr("id", "Dates_" + indexDel + "Times_" + indexTimeMinus + "__Id");
            h.attr('name', "Dates[" + indexDel + "].Times[" + indexTimeMinus + "].Id");

            indexTimeMinus++;
            position++;
        }
    }


    $("#Dates_" + indexDel + "Times_" + indexTime).remove();
    $("#Dates_" + indexDel + "Times_" + indexTime + "__Id").remove();
    $('#' + id).remove();
};