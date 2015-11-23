var APP = APP || {};

// returns configuration object for bootstrap datepickers
APP.getDarePickerConfig = function (){
    return {
        format: 'yyyy-mm-dd',
        startDate: '+1d',
        linked: true
        // TODO : add other necessary configuration once css for datepicker is added
    }
}

$( document ).ready(function() {
    console.log('Initializing datepickers...');

    // prevents browsers from adding default datepickers
    $('input[type="date"]').on('click', function (e) { e.preventDefault(); });

    // adds bootstrap datepicker to all input.datepicker elements on page
    $('input.datepicker').datepicker(APP.getDarePickerConfig());
});

function AddDate() {
    var m = $('span#divDate span:last-of-type').attr('id');
    console.log(m + " id");
    window.indexx = 1;
    if (m != null && m.length > 0) {
        window.indexx = m.substring(7);
        window.indexx++;
    }

    // container for times
    var idTime = 0;
    var container = $('#divDate');
    var element = "<br id='br"+window.indexx+"'>";
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
    element = $('#Dates_0__Times_0__Id').clone();
    element.attr('id', "Dates_" + window.indexx + "Times_0__Id");
    element.attr('name', "Dates[" + window.indexx + "].Times[0].Id");
    container.append(element);

    //first time
    element = $('#Dates_0__Times_0__Time').clone();
    element.attr('id', "Dates_" + window.indexx + "Times_0");
    element.attr('name', "Dates[" + window.indexx + "].Times[0].Time");
    container.append(element);

    //delete button
    element = $('#btnDel-0_1').clone();
    element.attr('id', "btnDel-" + window.indexx + "_0");
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






