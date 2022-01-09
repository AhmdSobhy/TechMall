function edit_row(no) {
	document.getElementById("edit" + no).style.display = "none";

	var name = document.getElementById("name_row" + no);
	var order = document.getElementById("order_row" + no);
	var age = document.getElementById("age_row" + no);
	var date = document.getElementById("date_row" + no);
	var money = document.getElementById("money_row" + no);
	

	var name_data = name.innerHTML;
	var order_data = order.innerHTML;
	var age_data = age.innerHTML;
	var date_data = date.innerHTML;
	var money_data = money.innerHTML;

	name.innerHTML = "<input type='text' id='name_text" + no + "' value='" + name_data + "'>";
	order.innerHTML = "<input type='text' id='country_text" + no + "' value='" + order_data + "'>";
	age.innerHTML = "<input type='text' id='age_text" + no + "' value='" + age_data + "'>";
	date.innerHTML = "<input type='text' id='date_text" + no + "' value='" + date_data + "'>";
	money.innerHTML = "<input type='text' id='money_text" + no + "' value='" + money_data + "'>";
}
function edit_row_seller(no) {
	document.getElementById("edit" + no).style.display = "none";

	var name = document.getElementById("name_row" + no);
	var order = document.getElementById("order_row" + no);
	var date = document.getElementById("date_row" + no);
	var money = document.getElementById("money_row" + no);


	var name_data = name.innerHTML;
	var order_data = order.innerHTML;
	var date_data = date.innerHTML;
	var money_data = money.innerHTML;

	name.innerHTML = "<input type='text' id='name_text" + no + "' value='" + name_data + "'>";
	order.innerHTML = "<input type='text' id='country_text" + no + "' value='" + order_data + "'>";
	date.innerHTML = "<input type='text' id='date_text" + no + "' value='" + date_data + "'>";
	money.innerHTML = "<input type='text' id='money_text" + no + "' value='" + money_data + "'>";
}

function delete_row(no) {
	document.getElementById("row" + no + "").outerHTML = "";
}
