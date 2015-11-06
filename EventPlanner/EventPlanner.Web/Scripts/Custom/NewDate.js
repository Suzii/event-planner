
function AddDate() {
    var m = $('span#divDate span:last-of-type').attr('id');
    var indexx = 1;
    console.log(m + " delka");
    if (m != null && m.length > 0) {
        indexx = m.substring(7);
        indexx++;
    }

    // container for times
    var idTime = 0;
    container = $('#divDate');
    element = "<br>";
    container.append(element);

    element = "<span id='divcust" + indexx + "'>";
    container.append(element);

    //datepicker
    var element = $('#Dates_0__Date').clone();
    console.log("date " + indexx);

    var container = $('#divcust'+indexx);
        element.attr('id', "Dates" + indexx);
        element.attr('name', "Dates" + indexx);
        container.append(element);

    //first time
        element = $('#Dates_0__Times_0_').clone();
        element.attr('id', "Times" + idTime);
        element.attr('name', "Times" + idTime);
        container.append(element);

    //delete button
        element = $('#btnDel1').clone();
        element.attr('id', "btnDel" + indexx);
        element.attr('name', "btnDel" + indexx);
        container.append(element);

        element = "</span >";
        container.append(element);

    //add time
        container = $('#divDate');
        element = $('#btnAdd0').clone();
        element.attr('id', "btnAdd" + indexx);
        element.attr('name', "btnAdd" + indexx);
        container.append(element);
};






  