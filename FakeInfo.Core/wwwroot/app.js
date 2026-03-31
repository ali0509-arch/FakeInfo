const generatePersonBtn = document.getElementById("generatePersonBtn");
const generateBulkBtn = document.getElementById("generateBulkBtn");

const bulkCountInput = document.getElementById("bulkCount");

const singleResult = document.getElementById("singleResult");
const bulkResult = document.getElementById("bulkResult");
const errorMessage = document.getElementById("errorMessage");

// === GENERÉR 1 PERSON ===
generatePersonBtn.addEventListener("click", async () => {
    clear();

    try {
        const res = await fetch("/api/person/full");

        if (!res.ok) throw new Error("Kunne ikke hente data");

        const person = await res.json();
        renderSingle(person);

    } catch (err) {
        showError(err.message);
    }
});

// === GENERÉR BULK ===
generateBulkBtn.addEventListener("click", async () => {
    clear();

    const count = parseInt(bulkCountInput.value);

    if (count < 2 || count > 100) {
        showError("Antal skal være mellem 2 og 100");
        return;
    }

    try {
        const res = await fetch(`/api/person/bulk?count=${count}`);

        if (!res.ok) throw new Error("Kunne ikke hente bulk data");

        const persons = await res.json();

        renderBulk(persons);

    } catch (err) {
        showError(err.message);
    }
});

// === RENDER SINGLE ===
function renderSingle(p) {
    singleResult.classList.remove("hidden");

    singleResult.innerHTML = createHtml(p);
}

// === RENDER BULK ===
function renderBulk(list) {
    bulkResult.innerHTML = "";

    list.forEach((p, i) => {
        const div = document.createElement("div");
        div.className = "card";

        div.innerHTML = `<h3>Person ${i + 1}</h3>` + createHtml(p);

        bulkResult.appendChild(div);
    });
}

// === HTML TEMPLATE ===
function createHtml(p) {
    const a = p.address || {};

    return `
        <div class="line"><b>Navn:</b> ${p.firstName} ${p.lastName}</div>
        <div class="line"><b>Køn:</b> ${p.gender}</div>
        <div class="line"><b>CPR:</b> ${p.cpr}</div>
        <div class="line"><b>Fødselsdato:</b> ${formatDate(p.dateOfBirth)}</div>
        <div class="line"><b>Telefon:</b> ${p.phone}</div>
        <div class="line">
            <b>Adresse:</b>
            ${a.street} ${a.number}, ${a.floor} ${a.door}, ${a.postalCode} ${a.town}
        </div>
    `;
}

// === FORMAT DATE ===
function formatDate(d) {
    if (!d) return "";
    return new Date(d).toLocaleDateString("da-DK");
}

// === UTILS ===
function clear() {
    errorMessage.textContent = "";
    singleResult.innerHTML = "";
    bulkResult.innerHTML = "";
    singleResult.classList.add("hidden");
}

function showError(msg) {
    errorMessage.textContent = msg;
}