import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { CreateCompany } from './components/CreateCompany';
import { CreateSupplier } from './components/CreateSupplier';
import { ToastContainer } from 'react-toastify';
import { ListSuppliers } from './components/ListSuppliers';


export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <ToastContainer />
                <Route exact path='/' component={Home} />
                <Route path='/createcompany' component={CreateCompany} />
                <Route path='/createsupplier' component={CreateSupplier} />
                <Route path='/listsuppliers' component={ListSuppliers} />
            </Layout>
        );
    }
}
