$(document).ready(function () {

    SetInputLayout();
    GetRackList();
    GetShelveList();

    function GetRackList() {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/ShelveRack/GetRackList',
            success: function (data) {
                SetRackDropdownlist(data.data);
            },
            error: function (data) {
            }
        })
    }

    function SetRackDropdownlist(rackListData) {
        var options = '<option value="-1">Please select</option>';
        for (var i = 0; i < rackListData.length; i++) {
            options += '<option value="' + rackListData[i].id + '">' + rackListData[i].name + '</option>';
        }

        $("#ddlSRackId").append(options);
    }

    function GetShelveList() {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/ShelveRack/GetShelveList',
            success: function (data) {
                InitializeDataTable(data.data);
            },
            error: function (data) {
            }
        })
    }

    function InitializeDataTable(data) {

        $('#ShelveDatatable').dataTable({
            "bDestroy": true,
            data: data,
            responsive: true,
            "deferRender": false,
            "ordering": false,
            "searching": false,
            columns: [
                { data: 'code' },
                { data: 'name' },
                { data: 'rackId' },
                { data: 'isDeleted' },
                { data: 'id' }
            ],
            columnDefs: [{
                targets: 4,
                width: "20%",
                render: function (data, type, row) {
                    return `<button class="btn btn-sm btn-danger delete-resource" data-id="${data}">Delete</button>`;
                }
            }]
        });
    }

    $(document).on("click", ".delete-resource", function () {
        var id = $(this).data("id");
        if (confirm('Are you sure you want to delete shelf?')) {
            $.ajax({
                type: "POST",
                contentType: 'application/x-www-form-urlencoded',
                dataType: "json",
                url: '/ShelveRack/Delete',
                data: { "id": id },
                success: function (response) {
                    debugger;
                    if (response.message != "success") {
                        alert("Error");
                    }
                    else {
                        GetShelveList();
                        $(".list-shelve-row").show();
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
        $(".create-shelve-row").hide();
        $(".list-shelve-row").show();
    }

    function CreateShelveLayout() {
        $("#btnCreate").show();
    }

    $("#btnCreateView").click(function (e) {
        $(window).scrollTop(0);
        ClearCreateModel();
        CreateShelveLayout();
        $(".create-shelve-row").show();
    });

    $("#btnCancelView").click(function (e) {
        ClearCreateModel();
        $(".create-shelve-row").hide();
    });

    function ClearCreateModel() {
        $("#txtSCode").val("");
        $("#txtSName").val("");
        $("#ddlSRackId").val("-1");
    }

    $(".btnCreateUpdate").click(function (e) {
        var data = GetShelveDataToCreate();
        if (data.RackId == "-1") {
            alert("Please select a Rack");
            return false;
        }
        $.ajax({
            type: "POST",
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            url: '/ShelveRack/CreateUpdate',
            data: data,
            success: function (response) {
                debugger;
                if (response.message != "success") {
                    alert("Error");
                }
                else {
                    $(".create-shelve-row").hide();
                    GetShelveList();
                    $(".list-shelve-row").show();

                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                window.open('/NotFound/NotFound');
            }
        });

    });

    function GetShelveDataToCreate() {
        var code = $("#txtSCode").val();
        var name = $("#txtSName").val();
        var rackId = $("#ddlSRackId").find(":selected").val();
        var data = { "Code": code, "Name": name, "RackId": rackId, "IsDeleted": false };
        return data;
    }
});