
function AddDate() {
    var m = $('span#divDate span:last-of-type').attr('id');
    window.indexx = 1;
    if (m != null && m.length > 0) {
        window.indexx = m.substring(7);
        window.indexx++;
    }

    // container for times
    var idTime = 0;
    var container = $('#divDate');
    var element = "<br>";
    container.append(element);

    element = "<span id='divcust" + window.indexx + "'>";
    container.append(element);

    //datepicker
    element = $('#Dates_0__Date').clone();
    container = $('#divcust'+window.indexx);
    element.attr('id', "Dates_" + window.indexx+"__Date_0");
        element.attr('name', "Dates[" + window.indexx+"].Date");
        container.append(element);

    //first time
        element = $('#Dates_0__Times_0_').clone();
        element.attr('id', "Dates_" + window.indexx + "Times_0");
        element.attr('name', "Dates[" + window.indexx + "].Times[0]");
        container.append(element);

    //delete button
        element = $('#btnDel-0_1').clone();
        element.attr('id', "btnDel-" + window.indexx+ "_0" );
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






  