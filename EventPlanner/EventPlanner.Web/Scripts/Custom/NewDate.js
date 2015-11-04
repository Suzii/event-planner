/*
function AddDate() {
    var m = $('span#divDate span:last-child').attr('id');
    var indexx = 1;
    if (m != null && m.length > 0) {
        indexx = m.substring(7);
        indexx++;
   }
    var html =
        "<input type='date' class='form-control' id='dp" + indexx + "' name='Dates" + indexx + "'/> <span id='divcust" + indexx + "' class='text-box single-line'> </span>" +
          "<input id='btnAdd"+indexx+"' type='button' value='Add time' onclick='AddTime(this.id);' name='DateSlots_"+indexx+"' />";
    $('#divDate').append(html);
};

*/

function AddDate() {
    var m = $('span#divDate input:last-child').attr('id');
    var indexx = 1;
    console.log(m + " delka");
    if (m != null && m.length > 0) {
        indexx = m.substring(6);
        indexx++;
    }
    //datepicker
    var element = $('#Dates_0__Date').clone();
    console.log("date " + indexx);

    var container = $('#divDate');
        element.attr('id', "Dates" + indexx);
        element.attr('name', "Dates" + indexx);
        container.append(element);

    // container for times
        var idTime = 0;
         element = "<span id='divcust" + indexx + "'></span>";
        container = $('#divdate');
        container.append(element);

    //first time
        element = $('#Dates_0__Times_0_').clone();
        console.log("ted " + idTime);
        container = $('#divcust'+indexx);
        element.attr('id', "Times" + idTime);
        element.attr('name', "Times" + idTime);
        container.append(element);

    //add time
        element = $('#btnAdd0').clone();
        container = $('#divdate');
        element.attr('id', "btnAdd" + indexx);
        element.attr('name', "btnAdd" + indexx);
        container.append(element);

};






  