document.addEventListener("DOMContentLoaded", () => {
    fetch('data/output.json')
        .then(response => {
            if (!response.ok) throw new Error("Network response was not ok");
            return response.json();
        })
        .then(data => {
            document.getElementById('last-updated').textContent =
                `Last updated: ${new Date(data.lastUpdated).toLocaleString()}`;

            populateTable('afc-table', data.afcSeeds);
            populateTable('nfc-table', data.nfcSeeds);
        })
        .catch(error => {
            console.error('Error loading playoff picture data:', error);
            document.getElementById('last-updated').textContent = 'Failed to load playoff data.';
        });
});

function populateTable(tableId, teams) {
    const tbody = document.querySelector(`#${tableId} tbody`);
    tbody.innerHTML = '';

    teams.forEach(team => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${team.seed}</td>
            <td><strong>${team.team}</strong></td>
            <td>${team.division}</td>
            <td>${team.record}</td>
            <td>${team.status}</td>
        `;
        tbody.appendChild(row);
    });
}