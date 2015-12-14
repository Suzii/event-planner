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
    //container = $('#divcust' + window.indexx);
    //var button = "<button id='btnDel-" + window.indexx + "' type='button' onclick='DelDate(this.id);' class='btn btn-default'>  <spann class='glyphicon glyphicon-remove' aria-hidden='true'/></ button>";
    //container.append(button);

    //datepicker
    container = $('#divcust' + window.indexx);
    element = $('#Dates_0__Date').clone();
    element.attr('id', "Dates_" + window.indexx + "__Date");
    element.attr('name', "Dates[" + window.indexx + "].Date");
    //prevent default browser datepicker
    element.on('click', function (e) { e.preventDefault(); });
    //add bootstrap datepicker
    element.datepicker(APP.getDarePickerConfig());
    container.append(element);

    
    //delete button
    var button = "<button id='btnDel-" + window.indexx + "' type='button' onclick='DelDate(this.id);' class='btn btn-default btn-xs'>  <spann class='glyphicon glyphicon-remove' aria-hidden='true'/></ button>";
    container.append(button);
    

    //first hidden
    element = $('#Dates_0Times_0Id').clone();
    element.attr('id', "Dates_" + window.indexx + "Times_0__Id");
    element.attr('name', "Dates[" + window.indexx + "].Times[0].Id");
    element.attr('value', '');
    container.append(element);

    //first time
    element = $('#Dates_0Times_0').clone();
    element.attr('id', "Dates_" + window.indexx + "Times_0");
    element.attr('name', "Dates[" + window.indexx + "].Times[0].Time");
    container.append(element);

    // commented out, so that every date has at least one time, that is not deletable
    ////delete button
    //element = $('#btnDel-0_1').clone();
    //if (element.length === 0) {
    //    element = "<button id='btnDel-" + window.indexx + "_0' type='button' onclick='DelTime(this.id);'  class='btn btn-default btn-xs'>  <spann class='glyphicon glyphicon-remove' aria-hidden='true' /></ button>";
    //} else {
    //    element.attr('id', "btnDel-" + window.indexx + "_0");
    //}
    //container.append(element);

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
  //  console.log(id + " id");
    var indexDel = id.substring(id.indexOf("-") + 1);
    console.log(indexDel + " index delete prvni");
    $("#divcust" + indexDel).remove();
    $("#btnAdd" + indexDel).remove();
    $("#br" + indexDel).remove();
    $('#' + id).remove();


    var indexTimeMinus = indexDel;
    indexTimeMinus++;
    //checked if it has siblings
    while ($("input#Dates_" + indexTimeMinus + "__Date").length > 0) {

        console.debug('indexTimeMinus=' + indexTimeMinus + '; indexDel=' + indexDel);

        var m = $("input#Dates_" + indexTimeMinus + "__Date");
  
        m.attr("id", "Dates_" + indexDel + "__Date");
        m.attr('name', "Dates[" + indexDel + "].Date");

        //change name of span      
        var n = $("span#divcust" + indexTimeMinus);
        n.attr("id", "divcust" + indexDel);

        //change name of delete button     
        n = $("button#btnDel-" + indexTimeMinus);
        n.attr("id", "btnDel-" + indexDel);
        console.log(n + " delt button");

        //change name of add time
        n = $("input#btnAdd" + indexTimeMinus);
        n.attr("id", "btnAdd" + indexDel);
        n.attr("name", "btnAdd" + indexDel);

        //change br
        n = $("br#br" + indexTimeMinus);
        n.attr("id", "br" + indexDel);
      
        //change times' name and id and hidden
        changeTimeId(indexDel);

        indexTimeMinus++;
        indexDel++;
       
    }

  
   
};

function changeTimeId(indexDel) {
    var index = indexDel;
    var indexDell = indexDel;
    index ++;
   var m = $("select#Dates_" + index + "Times_0").nextAll("select");

        for (var i = 0; i <= m.length +1; i++) {
            //change id and name
            var n = $("select#Dates_" + index + "Times_" + i);
            n.attr("id", "Dates_" + indexDell + "Times_" + i);
            n.attr('name', "Dates[" + indexDell + "].Times[" + i + "].Time");

            //change del button
            var b = $("select#Dates_" + indexDell + "Times_" + i).next("button");
            b.attr("id", "btnDel-" + indexDell + "_" + i);

            //change hidden
            var h = $("#Dates_" + index + "Times_" + i + "__Id");
            h.attr("id", "Dates_" + indexDell + "Times_" + i + "__Id");
            h.attr('name', "Dates[" + indexDell + "].Times[" + i + "].Id");

        }

}