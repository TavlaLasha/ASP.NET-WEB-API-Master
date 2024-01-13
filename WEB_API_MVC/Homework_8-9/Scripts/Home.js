const HomeModel = {
    UrlGetProducts: null,
    UrlGetProductCategories: null,
    UrlUpdateProductPrice: null,
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
                            <div class="card-body" data-id="${item.ID}">
                                <h5 class="text-center">${item.Name}</h5>
                                <p class="text-center">${item.Description ?? ''}</p>
                                <p class="text-center js-product-price">Price: <span class="js-product-price-value">${item.Price ?? ''}</span> <button class="btn js-product-price-edit-btn"><i class="fa fa-edit"></i></button></p>
                                <p class="text-center js-product-price-edit d-flex d-none"><span class="d-flex align-items-center">Price: </span> <input type="number" class="form-control w-50 ms-1 js-price-edit-input" value="${item.Price ?? ''}" /> <button class="btn js-product-price-edit-check-btn"><i class="fa fa-check"></i></button><button class="btn js-product-price-edit-cancel-btn"><i class="fa fa-close"></i></button></p>
                            </div>
                        </div>
                    </div>`);
                });
            }
            else {
                $('.js-products-row').html('No Products Found');
            }
        });
    },

    UpdateProductPricePromise: function (ProductID, NewPrice) {
        return new Promise(function (resolve, reject) {
            if (ProductID && NewPrice) {
                $.ajax({
                    type: 'POST',
                    url: HomeModel.UrlUpdateProductPrice,
                    dataType: 'json',
                    data: {
                        productID: ProductID,
                        newPrice: NewPrice
                    },
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
                    error: function (res) {
                        reject();
                    },
                    complete: function () {

                    }
                });
            }
            else {
                reject();
            }
        });
    },
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

    $('.js-products-row').on('click', '.js-product-price-edit-btn', function () {
        const Parent = $(this).closest(".card-body");
        const PriceInfoElement = Parent.find(".js-product-price");
        const PriceEditElement = Parent.find(".js-product-price-edit");
        PriceInfoElement.addClass("d-none");
        PriceEditElement.removeClass("d-none");
    });

    $('.js-products-row').on('click', '.js-product-price-edit-check-btn', function () {
        const Parent = $(this).closest(".card-body");
        const PriceInfoElement = Parent.find(".js-product-price");
        const PriceEditElement = Parent.find(".js-product-price-edit");
        const PriceEditInputElement = Parent.find(".js-price-edit-input");
        const NewPrice = PriceEditInputElement.val();
        PriceEditInputElement.removeClass("is-invalid");
        HomeModel.UpdateProductPricePromise(Parent.attr('data-id'), NewPrice).then(function () {
            Parent.find('.js-product-price-value').html(NewPrice);
            PriceInfoElement.removeClass("d-none");
            PriceEditElement.addClass("d-none");
        }, function () {
            PriceEditInputElement.addClass("is-invalid");
        });        
    });

    $('.js-products-row').on('click', '.js-product-price-edit-cancel-btn', function () {
        const Parent = $(this).closest(".card-body");
        const PriceInfoElement = Parent.find(".js-product-price");
        const PriceEditElement = Parent.find(".js-product-price-edit");
        PriceInfoElement.removeClass("d-none");
        PriceEditElement.addClass("d-none");
    });
});