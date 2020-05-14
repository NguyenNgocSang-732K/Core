getCookie()
setCookie()
phantrang()

*Để làm cái phân trang với chọn số dòng hiển thị trên 1 trang:
	- tạo 1 cái controller support, có cái action để set lại cái ConstantCuaSang rồi trả về giá trị dạng json
	- cần set số dòng hiển thị trên 1 trang vào 1 cái cookie("changeTotalRows")
	- đầu tiên khi load lên nếu cái cookie("changeTotalRows") đó chưa tồn tại thì mặc định nó bằng với giá trị của biến size trong ConstantCuaSang
	- mỗi lần chọn số dòng hiển thị trên 1 trang thì sẽ refresh trang đó, nên có 1 cái kiểm tra đầu tiên khi load là nếu cookie("changeTotalRows") chưa có, thì thôi, còn nếu có thì set cho cái <select> 
		có giá trị là cái cookie đó. $('.changQuantityRows').val(totalRows);
*Khi click dòng xem details thì gán cho cái <tr> đó 1 cái data-id, xong xuống viết function click cho nó chuyển tới trang đó bằng window.location.assign('/admin/account/' + this.getAttribute('data-rows'));