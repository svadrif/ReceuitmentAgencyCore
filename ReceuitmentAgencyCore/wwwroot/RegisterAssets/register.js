var btnUpload = $("#upload_file"),
	btnOuter = $(".button_outer");
btnUpload.on("change", function (e) {
	var ext = btnUpload.val().split('.').pop().toLowerCase();
	if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
		$(".error_msg").text("Not an Image...");
	} else {
		$(".error_msg").text("");
		btnOuter.addClass("file_uploading");
		setTimeout(function () {
			btnOuter.addClass("file_uploaded");
		}, 3000);
		var uploadedFile = URL.createObjectURL(e.target.files[0]);
		setTimeout(function () {
			$("#uploaded_view").append('<img src="' + uploadedFile + '" />').addClass("show");
		}, 3500);
	}
});
$(".file_remove").on("click", function (e) {
	$("#uploaded_view").removeClass("show");
	$("#uploaded_view").find("img").remove();
	btnOuter.removeClass("file_uploading");
	btnOuter.removeClass("file_uploaded");
});


/* selectlist */
$(document).ready(function () {
    var Country = $('#CountryId');
    var Region = $("#RegionId");
    var District = $("#DistrictId");
    Country.change(function () {
        var val = $(this).val();
        Region.html('');
        District.html('');
        if (val == "") {
            Region.attr("disabled", true);
            Region.append("<option value=\"\" selected disabled>Choose region</option>");
            District.attr("disabled", true);
            District.append("<option value=\"\" selected disabled>Choose district</option>");
        }
        else {
            $.getJSON("/GetList/GetRegionByCountryId/?countryId=" + val, function (data) {
                Region.append("<option value=\"\" selected disabled>Choose region</option>");
                District.append("<option value=\"\" selected disabled>Choose district</option>");
                $.each(data, function (index, dataValue) {
                    Region.append("<option value=\"" + dataValue.id + "\">" + dataValue.name + "</option>");
                })
            });
        }
    });
    Region.change(function () {
        var val = $(this).val();
        District.html('');
        if (val == "") {
            District.attr("disabled", true);
            District.append("<option value=\"\" selected disabled>Choose district</option>");
        }
        else {
            $.getJSON("/GetList/GetDistrictByRegionId/?regionId=" + val, function (data) {
                District.append("<option value=\"\" selected disabled>Choose district</option>");
                $.each(data, function (index, dataValue) {
                    District.append("<option value=\"" + dataValue.id + "\">" + dataValue.name + "</option>");
                })
            });
        }
    });


    $("#Phone").mask("(AA) AAA-AA-AA");
    $("#BirthDate").mask("AA-AA-AAAA");

});