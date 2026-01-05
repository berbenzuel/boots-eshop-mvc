(() => {
    const input = document.getElementById("imageInput");
    const list = document.getElementById("imageList");
    const existingInputsContainer =
        document.getElementById("existingImagesInputs");

    if (!list) return;

    // Unified state
    let items = [];
    // item:
    // { type: "existing", id, src }
    // { type: "new", file, src }

    /* -----------------------------------------
       INIT
    ----------------------------------------- */

    document.addEventListener("DOMContentLoaded", async () => {
        const existing = JSON.parse(list.dataset.existing || "[]");

        if (existing.length > 0) {
            await preloadExisting(existing);
        }

        render();
    });

    if (input) {
        input.addEventListener("change", () => {
            for (const file of input.files) {
                items.push({
                    type: "new",
                    file,
                    src: URL.createObjectURL(file)
                });
            }
            render();
        });
    }

    /* -----------------------------------------
       PRELOAD EXISTING (EDIT PAGE ONLY)
    ----------------------------------------- */

    async function preloadExisting(urls) {
        for (const url of urls) {
            try {
                const res = await fetch(url);
                if (!res.ok) continue;

                const blob = await res.blob();
                items.push({
                    type: "existing",
                    id: url,
                    src: URL.createObjectURL(blob)
                });
            } catch {
                /* ignore failed image */
            }
        }
    }

    /* -----------------------------------------
       RENDER (SYNC, ORDER SAFE)
    ----------------------------------------- */

    function render() {
        list.innerHTML = "";
        existingInputsContainer?.replaceChildren();

        items.forEach((item, index) => {
            const col = document.createElement("div");
            col.className = "col-6 col-md-3";

            const card = document.createElement("div");
            card.className = "image-card";
            card.draggable = true;
            card.dataset.index = index;

            if (index === 0) {
                card.classList.add("main");
                const badge = document.createElement("div");
                badge.className = "badge-main";
                badge.textContent = "Hlavní";
                card.appendChild(badge);
            }

            const img = document.createElement("img");
            img.src = item.src;

            const del = document.createElement("button");
            del.type = "button";
            del.className = "btn btn-sm btn-danger btn-delete";
            del.textContent = "✕";
            del.onclick = () => remove(index);

            card.append(img, del);
            setupDrag(card);

            col.appendChild(card);
            list.appendChild(col);

            if (item.type === "existing" && existingInputsContainer) {
                const hidden = document.createElement("input");
                hidden.type = "hidden";
                hidden.name = "ExistingImages";
                hidden.value = item.id;
                existingInputsContainer.appendChild(hidden);
            }
        });

        syncInput();
    }

    /* -----------------------------------------
       DRAG & DROP
    ----------------------------------------- */

    function setupDrag(card) {
        card.addEventListener("dragstart", e =>
            e.dataTransfer.setData("text/plain", card.dataset.index)
        );

        card.addEventListener("dragover", e => e.preventDefault());

        card.addEventListener("drop", e => {
            e.preventDefault();

            const from = +e.dataTransfer.getData("text/plain");
            const to = +card.dataset.index;

            items.splice(to, 0, items.splice(from, 1)[0]);
            render();
        });
    }

    /* -----------------------------------------
       REMOVE
    ----------------------------------------- */

    function remove(index) {
        const item = items[index];
        if (item?.src?.startsWith("blob:")) {
            URL.revokeObjectURL(item.src);
        }
        items.splice(index, 1);
        render();
    }

    /* -----------------------------------------
       SYNC FILE INPUT
    ----------------------------------------- */

    function syncInput() {
        if (!input) return;

        const dt = new DataTransfer();
        items
            .filter(i => i.type === "new")
            .forEach(i => dt.items.add(i.file));

        input.files = dt.files;
    }
})();
