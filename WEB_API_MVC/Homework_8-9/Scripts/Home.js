const HomeModel = {
    UrlGetProducts: null,
    UrlGetProductCategories: null,
    GetProductsPromise: function (CategoryID, CategoryParentID) {
        urlAdition = "";
        if (CategoryID && CategoryParentID) {
            urlAdition = "?id=" + CategoryID + "?parentID=" + CategoryParentID;
        }
        else {
            if (CategoryID) {
                urlAdition = "?id=" + CategoryID;
            }
            if (CategoryParentID) {
                urlAdition = "?parentID=" + CategoryParentID;
            }
        }
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: HomeModel.UrlGetProducts + urlAdition,
                dataType: 'json',
                beforeSend: function () {

                },
                success: function (data) {
                    if (data.IsSuccess) {
                        resolve(data.Products);
                    }
                    else {
                        reject();
                    }
                },
                error: function () {
                    reject();
                },
                complete: function () {

                }
            });
        });
    },

    GetCategoriesPromise: function (ParentID) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: HomeModel.UrlGetProductCategories + `?id=${ParentID}`,
                dataType: 'json',
                beforeSend: function () {

                },
                success: function (data) {
                    if (data.IsSuccess) {
                        resolve(data.ProductCategories);
                    }
                    else {
                        reject();
                    }
                },
                error: function () {
                    reject();
                },
                complete: function () {

                }
            });
        });
    },

    InitSubCategories: function (ParentID) {
        $('.js-category-select option:not(.initial-filter)').remove();
        if (ParentID) {
            HomeModel.GetCategoriesPromise(ParentID).then(function (categories) {
                if (categories.length > 0) {
                    categories.forEach(function (item) {
                        $('.js-category-select').append(`<option value="${item.ProductCategoryID}">${item.ProductCategoryName}</option>`);
                    });
                }
            });
        }
    },
    InitProducts: function (CategoryID, CategoryParentID) {
        HomeModel.GetProductsPromise(CategoryID, CategoryParentID).then(function (products) {
            if (products.length > 0) {
                $('.js-products-row').html('');
                products.forEach(function (item) {
                    $('.js-products-row').append(`<div class="col-md-4 ">
                        <div class="card">
                            <div class="ccc">
                                <p class="text-center"><img src="https://www.pacificfoodmachinery.com.au/media/catalog/product/placeholder/default/no-product-image-400x400_8.png" class="imw"></p>

                            </div>
                            <div class="card-body">
                                <h5 class="text-center">${item.Name}</h5>
                                <p class="text-center">Price: 5.00</p>
                            </div>
                        </div>
                    </div>`);
                });
            }
            else {
                $('.js-products-row').html('No Products Found');
            }
        });
    }
}

$(function () {
    HomeModel.InitProducts();
    $('.js-parent-category-select').change(function () {
        const CategoryID = $(this).val();
        HomeModel.InitSubCategories(CategoryID);
        HomeModel.InitProducts(null, CategoryID);
    });
    $('.js-category-select').change(function () {
        const CategoryID = $(this).val();
        HomeModel.InitProducts(CategoryID, null);
    });
});