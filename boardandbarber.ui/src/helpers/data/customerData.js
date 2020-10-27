
const getCustomers = () => new Promise((resolve, reject) => {
    resolve([
        {
            id: 1,
            name: "nathan",
            birthday: "5/27/1985",
            favoriteBarber: "Jim",
            notes: "High and tight"
        },
        {
            id: 2,
            name: "Bill",
            birthday: "4/5/1999",
            favoriteBarber: "Karl",
            notes: "short on the sides and long on top"
        }
    ])
});

export default { getCustomers };
