const IMAGE_BASE_URL = "/Source/ProductImage?filename=";

const input = document.getElementById("imageInput");
const list = document.getElementById("imageList");
const orderInputs = document.getElementById("imageOrderInputs");

// Single array of items: { key, type: "existing"|"new", file? }
let items = (JSON.parse(list.dataset.existing || "[]"))
    .map(f => ({ key: f, type: "existing" }));

let newCounter = 0; // counter for NEW:i keys

render();

input.addEventListener("change", () => {
    Array.from(input.files).forEach(f => {
        items.push({ key: `NEW:${newCounter++}`, type: "new", file: f });
    });
    render();
});

function render() {
    list.innerHTML = "";
    orderInputs.innerHTML = "";

    items.forEach(item => {
        const col = document.createElement("div");
        col.className = "col-6 col-md-3";

        const card = document.createElement("div");
        card.className = "border p-2 position-relative";
        card.draggable = true;
        card.dataset.key = item.key;

        const img = document.createElement("img");
        img.className = "img-fluid";
        img.src = item.type === "existing"
            ? IMAGE_BASE_URL + item.key
            : URL.createObjectURL(item.file);

        const del = document.createElement("button");
        del.type = "button";
        del.className = "btn btn-danger btn-sm position-absolute top-0 end-0";
        del.textContent = "✕";
        del.onclick = () => remove(item.key);

        card.appendChild(img);
        card.appendChild(del);
        setupDrag(card);

        col.appendChild(card);
        list.appendChild(col);

        const hidden = document.createElement("input");
        hidden.type = "hidden";
        hidden.name = "ImageOrder";
        hidden.value = item.key;
        orderInputs.appendChild(hidden);
    });

    syncFileInput();
}

function remove(key) {
    items = items.filter(x => x.key !== key);
    render();
}

function setupDrag(card) {
    card.addEventListener("dragstart", e => {
        e.dataTransfer.setData("key", card.dataset.key);
    });

    card.addEventListener("dragover", e => e.preventDefault());

    card.addEventListener("drop", e => {
        e.preventDefault();
        const fromKey = e.dataTransfer.getData("key");
        const toKey = card.dataset.key;

        const fromIndex = items.findIndex(x => x.key === fromKey);
        const toIndex = items.findIndex(x => x.key === toKey);

        if (fromIndex === -1 || toIndex === -1) return;

        const [moved] = items.splice(fromIndex, 1);
        items.splice(toIndex, 0, moved);

        render();
    });
}

function syncFileInput() {
    const dt = new DataTransfer();
    items.filter(x => x.type === "new").forEach(x => dt.items.add(x.file));
    input.files = dt.files;
}


let rowIndex = document.querySelectorAll('#stockRows tr').length;

function addRow() {
    const tbody = document.getElementById('stockRows');

    const row = document.createElement('tr');
    row.innerHTML = `
        <input type="hidden" name="Rows[${rowIndex}].Id" value="" />

        <td>
            <select name="Rows[${rowIndex}].ProductColorId" class="form-select">
                ${colorOptions}
            </select>
        </td>

        <td>
            <select name="Rows[${rowIndex}].ProductSizeId" class="form-select">
                ${sizeOptions}
            </select>
        </td>

        <td>
            <input name="Rows[${rowIndex}].Quantity"
                   type="number"
                   min="0"
                   value="0"
                   class="form-control" />
        </td>

        <td class="text-center">
            <button type="button"
                    class="btn btn-danger"
                    onclick="removeRow(this)">
                Delete
            </button>
        </td>
    `;

    tbody.appendChild(row);
    rowIndex++;
}

function removeRow(btn) {
    btn.closest('tr').remove();
}
