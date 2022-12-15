function myFunction() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("table");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++)
    {
        tr[i].style.display = "none";
        for (j = 0; j < tr[i].getElementsByTagName("td").length; j++)
        {
            td = tr[i].getElementsByTagName("td")[j]; 
            if(td) {
                txtValue = td.textContent || td.innerText;
                if (txtValue.toUpperCase().indexOf(filter).toString() > -1) {
                    tr[i].style.display = "";
                }
            }
        }
    }
}

