import React from 'react';
import '.SingleCustomer.scss';

class SingleCustomer extends React.Component {
    render() {
        return(
            <>
                <strong>{this.props.name}</strong>
                <ul>
                    <li>Id: {this.props.id}</li>
                    <li>Birthday: {this.props.birthday}</li>
                    <li>Favorite Barber {this.props.favoriteBarber}</li>
                    <li>Notes: {this.props.notes}</li>
                </ul>
            </>
        )
    }
}