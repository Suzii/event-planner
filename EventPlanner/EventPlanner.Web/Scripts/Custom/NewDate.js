
function AddDate() {
    var m = $('span#divDate span:last-child').attr('id');
    var indexx = 1;
    if (m != null && m.length > 0) {
        indexx = m.substring(7);
        indexx++;
   }
    var html =
        "<input type='date' class='form-control' id='dp"+indexx+"' name='Date"+indexx+"'/> <span id='divcust"+indexx+"' class='text-box single-line'> </span>"+
          "<input id='btnAdd"+indexx+"' type='button' value='Add time' onclick='AddTime(this.id);' name='DateSlots_"+indexx+"' />";
    $('#divDate').append(html);
};




  