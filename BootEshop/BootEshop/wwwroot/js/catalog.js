
function normalizeFilterState(state) {
    return {
        categoryId: state?.categoryId ?? null,

        // IMPORTANT: keep everything as STRING (GUID-safe)
        sizes: Array.isArray(state?.sizes) ? state.sizes.map(String) : [],
        colorIds: Array.isArray(state?.colorIds) ? state.colorIds.map(String) : [],
        manufacturerIds: Array.isArray(state?.manufacturerIds) ? state.manufacturerIds.map(String) : [],

        minPrice: state?.minPrice ?? null,
        maxPrice: state?.maxPrice ?? null,
        sortBy: state?.sortBy ?? "Recommended"
    };
}


let filterState = normalizeFilterState(window.catalogFilter ?? {});


function toggleInArray(arr, value) {
    const i = arr.indexOf(value);
    if (i === -1) arr.push(value);
    else arr.splice(i, 1);
}


function applyFilterStateToUI() {

    document.querySelectorAll(".size-checkbox").forEach(cb => {
        cb.checked = filterState.sizes.includes(cb.dataset.sizeId);
    });

    document.querySelectorAll(".color-checkbox").forEach(cb => {
        cb.checked = filterState.colorIds.includes(cb.dataset.colorId);
    });

    document.querySelectorAll(".manufacturer-checkbox").forEach(cb => {
        cb.checked = filterState.manufacturerIds.includes(cb.dataset.manufacturerId);
    });

    document.querySelectorAll("#useMinPrice").forEach(cb => {
        cb.checked = filterState.minPrice !== null;
    });
    document.querySelectorAll("#minPrice").forEach(cb => {
        if(filterState.minPrice === null)
        {
            cb.disabled = true;
        }
        else {
            cb.value = filterState.minPrice;
        };
        
    });


    const sortSelect = document.getElementById("sort-select");
    if (sortSelect) {
        sortSelect.value = filterState.sortBy;
    }
    
    const categoryRadios = document.querySelectorAll(".category-radio");
    categoryRadios.forEach(radio => {
        radio.checked = (radio.dataset.categoryId === filterState.categoryId);
    });
}


function reloadGrid() {
    console.log("Reloading with filter:", filterState);

    fetch("/Catalog/CatalogGrid", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(filterState)
    })
        .then(r => r.text())
        .then(html => {
            const grid = document.getElementById("product-grid");
            if (!grid) return;

            grid.innerHTML = html;

            // Re-apply UI state after update
            filterState = normalizeFilterState(filterState);
            applyFilterStateToUI();
        })
        .catch(err => console.error("Reload failed:", err));
}

document.addEventListener("change", e => {
    const t = e.target;
    console.log(t);
    if (t.matches(".size-checkbox")) {
        toggleInArray(filterState.sizes, t.dataset.sizeId);
        reloadGrid();
    }

    else if (t.matches(".color-checkbox")) {
        toggleInArray(filterState.colorIds, t.dataset.colorId);
        reloadGrid();
    }

    else if (t.matches(".manufacturer-checkbox")) {
        toggleInArray(filterState.manufacturerIds, t.dataset.manufacturerId);
        reloadGrid();
    }

    else if (t.matches("#sort-select")) {
        filterState.sortBy = t.value;
        reloadGrid();
    }
    else if(t.matches(".category-radio")) {
        filterState.categoryId = t.dataset.categoryId;
        filterState.sizes = [];
        filterState.colorIds = [];
        filterState.manufacturerIds = [];
        reloadGrid();
    }
    else if (t.matches("#useMinPrice")) {
        console.log(t);
        const input = document.querySelector("#minPrice");
        input.disabled = !t.checked;
        if (t.checked) {
            filterState.minPrice = input.value;
        }
        else {
            filterState.minPrice = null;
        }
        
    }
});


document.addEventListener("DOMContentLoaded", () => {
    applyFilterStateToUI();
});





