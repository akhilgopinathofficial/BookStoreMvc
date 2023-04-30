$(document).ready(function () {

    SetInputLayout();
    GetShelfList();
    GetBookList();

    function GetShelfList() {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/ShelveRack/GetShelveList',
            success: function (data) {
                SetShelfDropdownlist(data.data);
            },
            error: function (data) {
            }
        })
    }

    function SetShelfDropdownlist(shelveListData) {
        var options = '<option value="-1">Please select</option>';
        for (var i = 0; i < shelveListData.length; i++) {
            options += '<option value="' + shelveListData[i].id + '">' + shelveListData[i].name + '</option>';
        }

        $("#ddlBShelfId").append(options);
    }

    $('#chkFilterDeleted').change(function () {
        var isDeleted = false;
        if (this.checked) {
            isDeleted = true;
        }
        GetBookList(isDeleted);
    });

    function GetBookList(isDeleted) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/Book/GetBookList?filterDeleted=' + isDeleted,
            success: function (data) {
                InitializeDataTable(data.data);
            },
            error: function (data) {
            }
        })
    }

    function InitializeDataTable(data) {

        $('#BookDatatable').dataTable({
            "bDestroy": true,
            data: data,
            responsive: true,
            "deferRender": false,
            "ordering": false,
            "searching": false,
            "footerCallback": function (row, data, start, end, display) {
                $('#BookDatatable tfoot').remove();
                var totalAmount = 0;
                for (var i = 0; i < data.length; i++) {
                    totalAmount += parseFloat(data[i].price);
                }
                var footer = $(this).append('<tfoot><tr><th style="width:200px" class="text-end">Total Price: ' + totalAmount +'</th></tr></tfoot>');
            },
            columns: [
                { data: 'code' },
                { data: 'name' },
                { data: 'isAvailable' },
                { data: 'price' },
                { data: 'shelfName' },
                { data: 'isDeleted' },
                { data: 'id'}
            ],
            columnDefs: [{
                targets: 6,
                width: "20%",
                render: function (data, type, row) {
                    var isDeleted = row['isDeleted'];
                    if (!isDeleted) {
                        return `<button class="btn btn-sm btn-primary view-resource" data-id="${data}">View</button>
                            <button class="btn btn-sm btn-outline-dark edit-resource" data-id="${data}">Edit</button>
                        <button class="btn btn-sm btn-danger delete-resource" data-id="${data}">Delete</button>
                    `;
                    }
                    return '';
                }
            }]
        });
    }

    $(document).on("click", ".edit-resource", function () {
        var id = $(this).data("id");
        $.ajax({
            type: "POST",
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            url: '/Book/Edit',
            data: { "id": id},
            success: function (response) {
                debugger;
                if (response.message != "success") {
                    alert("Error");
                }
                else {
                    EditBookLayout();
                    $(".create-update-book-row").show();
                    SetEditBookData(response.data);
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                window.open('/NotFound/NotFound');
            }
        });

    });

    $(document).on("click", ".view-resource", function () {
        var id = $(this).data("id");
        $.ajax({
            type: "POST",
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            url: '/Book/View',
            data: { "id": id },
            success: function (response) {
                debugger;
                if (response.message != "success") {
                    alert("Error");
                }
                else {
                    ViewBookLayout();
                    $("#downloadControl").attr("href", '/Book/DownloadPDFSample?id=' + id);
                    SetViewBookData(response.data);
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                window.open('/NotFound/NotFound');
            }
        });
    });

    $(document).on("click", ".delete-resource", function () {
        var id = $(this).data("id");
        if (confirm('Are you sure you want to delete book?')) {
            $.ajax({
                type: "POST",
                contentType: 'application/x-www-form-urlencoded',
                dataType: "json",
                url: '/Book/Delete',
                data: { "id": id },
                success: function (response) {
                    debugger;
                    if (response.message != "success") {
                        alert("Error");
                    }
                    else {
                        GetBookList();
                        $(".list-book-row").show();
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    window.open('/NotFound/NotFound');
                }
            });
        } else {
            return;
        }
    });

    function SetInputLayout() {
        ListBookLayout();
        $(".create-update-book-row").hide();
        $(".view-book-row").hide();
        $(".list-book-row").show();
    }

    function CreateBookLayout() {
        $("#btnCreate").show();
        $("#btnUpdate").hide();
        $(".create-book-title").show();
        $(".edit-book-title").hide();
    }
    function EditBookLayout() {
        $("#btnCreate").hide();
        $("#btnUpdate").show();
        $(".create-book-title").hide();
        $(".edit-book-title").show();
    }
    function ViewBookLayout() {
        $(".view-book-row").show();
    }
    function ListBookLayout() {

    }

    $("#btnCreateView").click(function (e) {
        $(window).scrollTop(0);
        ClearCreateModel();
        CreateBookLayout();
        $(".create-update-book-row").show();
    });

    $("#btnCancelView").click(function (e) {
        ClearCreateModel();
        $(".create-update-book-row").hide();
        $(".view-book-row").hide();
    });

    $("#btnBookViewCancel").click(function (e) {
        $(".view-book-row").hide();
    });

    function ClearCreateModel()
    {
        $("#txtBCode").val("");
        $("#txtBName").val("");
        $("#txtBPrice").val("");
        $("#ddlBShelfId").val("-1");
        $("#chkBAvailable").val("");
    }

    $(".btnCreateUpdate").click(function (e) {
        var data = GetBookDataToCreate();
        if (data.ShelfId == "-1") {
            alert("Please select a shelf");
            return false;
        }
        $.ajax({
            type: "POST",
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            url: '/Book/CreateUpdate',
            data: data,
            success: function (response) {
                debugger;
                if (response.message != "success") {
                    alert("Error");
                }
                else {
                    $(".create-update-book-row").hide();
                    GetBookList();
                    $(".list-book-row").show();

                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                window.open('/NotFound/NotFound');
            }
        });

    });

    function SetViewBookData(data) {
        $("#vCode").val(data.code);
        $("#vName").val(data.name);
        $("#vPrice").val(data.price);
        $("#vShelf").val(data.shelfId);
        $("#vAvailable").val(data.isAvailable);
    }

    function SetEditBookData(data) {
        $("#BookId").val(data.id);
        $("#txtBCode").val(data.code);
        $("#txtBName").val(data.name);
        $("#txtBPrice").val(data.price);
        $("#ddlBShelfId").val(data.shelfId);
        $("#chkBAvailable").prop('checked', data.isAvailable);
    }

    function GetBookDataToCreate() {
        var isAvailable = false;
        var id = $("#BookId").val();
        var code = $("#txtBCode").val();
        var name = $("#txtBName").val();
        var price = $("#txtBPrice").val();
        var shelveId = $("#ddlBShelfId").find(":selected").val();
        if ($('#chkBAvailable').is(":checked")) {
            isAvailable = true;
        }
        var data = { "Id": id, "Code": code, "Name": name, "IsAvailable": isAvailable, "Price": price, "ShelfId": shelveId, "IsDeleted": false };
        return data;
    }

    $("#btnDownloadBook").click(function (e) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/Book/DownloadFile',
            success: function (data) {
                
            },
            error: function (data) {
            }
        })

    });
});