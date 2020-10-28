import React from 'react';
import './Customers.scss';

import CustomerData from '../../../helpers/data/customerData';

import SingleCustomer from '../../shared/SingleCustomer/SingleCustomer';

class Customers extends React.Component {
    state = {
        customers: []
    }

    componentDidMount() {
        CustomerData.getCustomers()
        .then(customers => { this.setState({ customers }) })
    }

    render() {
        const { customers } = this.state;

        const buildCustomerList = customers.map((customer) => (
            <SingleCustomer key={customer.id} customer={customer} />
        ));
        return(
            <>
                {buildCustomerList}
            </>
        )
    }
}

export default Customers;