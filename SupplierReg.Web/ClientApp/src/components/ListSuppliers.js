import React, { Component } from 'react';
import { toast } from 'react-toastify';
import Moment from 'moment';
import { DateTimePicker } from 'react-widgets'
import MomentLocaliser from 'react-widgets-moment'

MomentLocaliser(Moment)

export class ListSuppliers extends Component {
    static displayName = ListSuppliers.name;

    constructor(props) {
        super(props);
        this.state = {
            suppliers: [],
            loading: true,
            filterName: "",
            filterCpfcnpj: "",
            filterCreationDate: null,
            filterCreationString: ""
        };

        this.fetchData();
    }

    fetchData = () => {
        var url = new URL("https://localhost:44307/suppliers");
        var params = { name: this.state.filterName, cpfcnpj: this.state.filterCpfcnpj, creationDate: this.state.filterCreationString };
        Object.keys(params).forEach(key => {
            if (params[key] != null) {
                url.searchParams.append(key, params[key])
            }
        })

        this.setState({
            loading: true
        });

        fetch(url)
            .then((response) => {
                const ok = response.ok;
                const data = response.json();
                return Promise.all([ok, data]).then(res => ({
                    ok: res[0],
                    data: res[1]
                }));
            })
            .then(responseJson => {

                if (responseJson.ok) {
                    this.setState({
                        suppliers: responseJson.data.map((supplier) => {
                            supplier.creationDate = Moment(supplier.creationDate).format('DD/MM/YYYY');
                            return supplier;
                        }),
                        loading: false
                    });
                } else {
                    if (responseJson.data != null) {
                        responseJson.data.map((error, i) => {
                            toast.error(error.message);
                        });
                    } else {
                        toast.error("Ocorreu um erro.");
                    }
                }
            })
            .catch((error) => {
                console.error(error);
            });
    };

    changeHandler = e => {
        this.setState({ [e.target.name]: e.target.value });
    }

    changeCreationDateHandler = date => {
        if (date == null) {
            this.setState({
                filterCreationDate: null,
                filterCreationString: ""
            });
            return;
        }

        var filterdate = Moment(date).format('YYYY-MM-DD');
        this.setState({
            filterCreationDate: date,
            filterCreationString: filterdate
        });
    }

    submitFilterHandler = e => {
        e.preventDefault();
        this.fetchData();
    }

    static renderSuppliers(suppliers) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Fornecedor</th>
                        <th>CPF/CNPJ</th>
                        <th>Empresa</th>
                        <th>Data Criacao</th>
                    </tr>
                </thead>
                <tbody>
                    {suppliers.map(supplier =>
                        <tr key={supplier.cpfcnpj}>
                            <td>{supplier.name}</td>
                            <td>{supplier.cpfcnpj}</td>
                            <td>{supplier.companyName}</td>
                            <td>{supplier.creationDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ListSuppliers.renderSuppliers(this.state.suppliers);

        const { filterName, filterCpfcnpj, filterCreationDate } = this.state

        var filterButtonStyle = {
            float: 'right'
        };

        var filterLabelStyle = {
            paddingRight: '5px'
        };

        return (
            <div>
                <div className="row">
                    <h1>Fornecedores</h1>
                </div>
                <br />
                <div>
                    <div className="row">
                        <h2>Filtros</h2>
                    </div>
                    <form onSubmit={this.submitFilterHandler}>
                        <div className="row">

                            <div className="col form-group">
                                <label style={filterLabelStyle}>Fornecedor</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    name="filterName"
                                    value={filterName}
                                    onChange={this.changeHandler}
                                />
                            </div>
                            <div className="col form-group">
                                <label style={filterLabelStyle}>CPF/CNPJ</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    name="filterCpfcnpj"
                                    value={filterCpfcnpj}
                                    onChange={this.changeHandler}
                                />
                            </div>
                            <div className="col form-group">
                                <label style={filterLabelStyle}>Data Criacao</label>
                                <DateTimePicker
                                    format="DD/MM/YYYY"
                                    value={filterCreationDate}
                                    onChange={this.changeCreationDateHandler}
                                    time={false}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-1 offset-11">
                                <button className="btn btn-primary" type="submit" style={filterButtonStyle}>Filtrar</button>
                            </div>
                        </div>
                    </form>
                </div>
                <br />
                {contents}
            </div>
        );
    }
}
