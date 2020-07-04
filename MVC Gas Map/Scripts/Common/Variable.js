//------------------STRING
const STR_DEFAULT_NAME_GUEST = "Người dùng";
const STR_WARNING_EMPTY = "Chưa nhập thông tin";
const STR_MESS_ERROR_LOAD_FAIL = "Truy cập dữ liệu thất bại";
const STR_HREF_ERROR_404_PAGE = "/Error/PageNotFound";
const STR_OSM_URL = 'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
const STR_OSM_ATTRIBUTE = 'Có làm mới có ăn !!! ';



//------------------INTEGER
const INT_ZOOM_MAP_LEVEL = 15;
const INT_FLAGCHECK_FAIL = 0;
const INT_FLAGCHECK_SUCCESS = 1;

//----------------------ARRAY
const ARR_PAGE_GO_TO_404_PAGE =
    [
        "/Guest/Index",
        "/Guest/Details",
        "/Store/Details",
        "/Store/Create",
        "/User/Edit",
    ];

const ARR_DEFAULT_COORD = [21.061540, 105.781103];//toạ độ mặc địNH Ở PHẠM VĂN ĐỒNG

//----------------------ENUM
class objPro {
    constructor(vlu, txt) {
        this.Value = vlu;
        this.Text = txt;
    }
}

const PERMISSION = {
    Undefined: 0,
    Unknown: 1,
    Guest: 2,
    Store:
    {
        NoStore: 3,
        HaveStore: 4
    },
    Admin: 5
};


const REQUEST_FROM_GUEST = {
    Refer: new objPro(0,"Tư vấn"),
    Repair: new objPro(1, "Sửa chữa"),
    Buy: {
        Gas: new objPro(2, "Mua bình gas"),
        Stove: new objPro(3, "Mua bếp gas"),
        Other: new objPro(4, "Mua các vật dụng khác")
    },
    Sell :
    {
        Gas: new objPro(5, "Bán bình gas"),
        Stove: new objPro(6, "Bán bếp gas"),
        Other: new objPro(7, "Bán các vật dụng khác")
    }
}

