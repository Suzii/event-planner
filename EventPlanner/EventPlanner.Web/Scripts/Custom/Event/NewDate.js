var APP = APP || {};

// returns configuration object for bootstrap datepickers
APP.getDarePickerConfig = function () {
    return {
        format: 'yyyy-mm-dd',
        startDate: '+1d',
        linked: true
        // TODO : add other necessary configuration once css for datepicker is added
    }
}

$(document).ready(function () {
    console.log('Initializing datepickers...');

    // prevents browsers from adding default datepickers
    $('input[type="date"]').on('click', function (e) { e.preventDefault(); });

    // adds bootstrap datepicker to all input.datepicker elements on page
    $('input.datepicker').datepicker(APP.getDarePickerConfig());
});

function AddDate() {
    var m = $('span#divDate span:last-of-type').attr('id');
    window.indexx = 1;
    if (m != null && m.length > 0) {
        window.indexx = m.substring(7);
        window.indexx++;
    }

    // container for times
    var container = $('#divDate');
    var element = "<br id='br" + window.indexx + "'>";
    container.append(element);

    element = "<span id='divcust" + window.indexx + "'>";
    container.append(element);

    //delete button
    container = $('#divcust' + window.indexx);
    var button = "<button id='btnDel-" + window.indexx + "' type='button' onclick='DelDate(this.id);' class='btn btn-default'>  <spann class='glyphicon glyphicon-remove' aria-hidden='true'/></ button>";
    container.append(button);

    //datepicker
    element = $('#Dates_0__Date').clone();
    element.attr('id', "Dates_" + window.indexx + "__Date_0");
    element.attr('name', "Dates[" + window.indexx + "].Date");
    //prevent default browser datepicker
    element.on('click', function (e) { e.preventDefault(); });
    //add bootstrap datepicker
    element.datepicker(APP.getDarePickerConfig());
    container.append(element);



    //first hidden
    element = $('#Dates_0Times_0Id').clone();
    element.attr('id', "Dates_" + window.indexx + "Times_0__Id");
    element.attr('name', "Dates[" + window.indexx + "].Times[0].Id");
    container.append(element);

    //first time
    element = $('#Dates_0Times_0').clone();
    element.attr('id', "Dates_" + window.indexx + "Times_0");
    element.attr('name', "Dates[" + window.indexx + "].Times[0].Time");
    container.append(element);

    //delete button
    element = $('#btnDel-0_1').clone();
    if (element.length === 0) {
        element = "<button id='btnDel-" + window.indexx + "_0' type='button' onclick='DelTime(this.id);'  class='btn btn-default btn-xs'>  <spann class='glyphicon glyphicon-remove' aria-hidden='true' /></ button>";
    } else {
        element.attr('id', "btnDel-" + window.indexx + "_0");
    }
    container.append(element);

    element = "</span >";
    container.append(element);

    //add time
    container = $('#divDate');
    element = $('#btnAdd0').clone();
    element.attr('id', "btnAdd" + window.indexx);
    element.attr('name', "btnAdd" + window.indexx);
    container.append(element);

    window.idTime = 1;
};

function DelDate(id) {
    var indexDel = id.substring(id.indexOf("-") + 1);
    var indexTimeMinus = indexDel;
    /*
    //checked if it has siblings
    while ($("input#Dates_" + indexDel + "__Date")) {
        var m = $("input#Dates_" + indexTimeMinus + "__Date");
        m.attr("id", "Dates_" + indexTimeMinus + "__Date");
        m.attr('name', "Dates[" + indexTimeMinus + "].Date");

        //change name of span
        n = $("span#divcust" + indexTimeMinus);
        n.attr("id", "divcust" + indexTimeMinus);

        indexTimeMinus++;
    }

    var m = $("input#Dates_" + indexDel + "__Date").nextAll("select");

    if (m.length != 0) {

        var indexTimeMinus = indexTime;
        var position = indexTime;
        position++;
        for (var i = 0; i <= m.length; i++) {
            //change id and name
            n = $("select#Dates_" + indexDel + "Times_" + position);
            n.attr("id", "Dates_" + indexDel + "Times_" + indexTimeMinus);
            n.attr('name', "Dates[" + indexDel + "].Times[" + indexTimeMinus + "].Time");
            //change del button
            b = $("select#Dates_" + indexDel + "Times_" + indexTimeMinus).next("button");
            b.attr("id", "btnDel-" + indexDel + "_" + indexTimeMinus);
            //change hidden
            h = $("#Dates_" + indexDel + "Times_" + position + "__Id");
            h.attr("id", "Dates_" + indexDel + "Times_" + indexTimeMinus + "__Id");
            h.attr('name', "Dates[" + indexDel + "].Times[" + indexTimeMinus + "].Id");

            indexTimeMinus++;
            position++;
        }
    }*/

    $("#divcust" + indexDel).remove();
    $("#btnAdd" + indexDel).remove();
    $("#br" + indexDel).remove();
    $('#' + id).remove();
};







