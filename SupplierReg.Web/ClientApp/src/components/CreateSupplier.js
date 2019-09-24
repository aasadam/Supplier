import React, { Component } from 'react';
import { toast } from 'react-toastify';
import { Combobox } from 'react-widgets'
import { DateTimePicker } from 'react-widgets'
import Moment from 'moment'
import MomentLocaliser from 'react-widgets-moment'

MomentLocaliser(Moment)


export class CreateSupplier extends Component {
    static displayName = CreateSupplier.name;

    constructor(props) {
        super(props);

        this.state = {
            companyid: '',
            name: '',
            cpfcnpj: '',
            birthdate: null,
            birthstring: '',
            rg: '',
            privateindividual: 'false',
            phones: [],
            companies: [],
            newphone: '',
            newphoneisresidential: false
        };

        this.fetchCompaniesData();
    }

    fetchCompaniesData = () => {
        fetch("https://localhost:44307/companies")
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
                        companies: responseJson.data.map((company) => {
                            return {
                                companyid: company.companyID,
                                companyname: company.name
                            };
                        })
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

    changeBirthDateHandler = date => {
        if (date == null) {
            this.setState({
                birthdate: null,
                birthstring: ""
            });
            return;
        }

        var filterdate = Moment(date).format('YYYY-MM-DD');
        this.setState({
            birthdate: date,
            birthstring: filterdate
        });
    }

    changeHandler = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    changeCheckBoxHandler = e => {
        this.setState({ [e.target.name]: e.target.checked })
    }

    submitHandler = e => {
        e.preventDefault();
        console.log(this.state);
        fetch('https://localhost:44307/suppliers', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.state)
        })
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
                    toast.success("Fornecedor cadastrado com sucesso.");
                    this.setState({
                        companyid: '',
                        name: '',
                        cpfcnpj: '',
                        birthdate: null,
                        birthstring: '',
                        rg: '',
                        privateindividual: 'false',
                        phones: [],
                        newphone: '',
                        newphoneisresidential: false
                    });
                } else {
                    if (responseJson.data != null) {
                        responseJson.data.map((error, i) => {
                            toast.error(error.message);
                        });
                    } else {
                        toast.error("Ocorreu um erro ao salvar fornecedor.");
                    }
                }
            })
            .catch((error) => {
                console.error(error);
            });
    }

    AddCurrentPhone = () => {
        var newphone = { phonenumber: this.state.newphone, isresidential: this.state.newphoneisresidential };
        this.setState({
            phones: [...this.state.phones, newphone]
        });
    }

    render() {
        var buttonStyle = {
            paddingTop: '26px'
        };

        var leftRadioStyle = {
            marginLeft: '20px'
        }

        var phoneCheckStyle = {
            marginTop: '45px'
        }

        var phoneOthersStyle = {
            marginTop: '40px'
        }

        return (
            <div>
                <div>
                    <h1>Cadastrar Fornecedor</h1>
                </div>
                <br />
                <form onSubmit={this.submitHandler}>
                    <div className="row">
                        <div className="col-3 form-group">
                            <label>Empresa</label>
                            <Combobox
                                data={this.state.companies}
                                valueField='companyid'
                                textField='companyname'
                                defaultValue={this.state.companyid}
                                onChange={value => this.setState({ companyid: value.companyid })}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-3 form-group" style={leftRadioStyle}>
                            <input type="radio"
                                name="privateindividual"
                                id="privateindividual1"
                                className="form-check-input"
                                value={false}
                                checked={this.state.privateindividual == 'false'}
                                onChange={this.changeHandler} />
                            <label className="form-check-label">Pessoa Juridica</label>
                        </div>
                        <div className="col-3 form-group">
                            <input type="radio"
                                name="privateindividual"
                                id="privateindividual2"
                                className="form-check-input"
                                value={true}
                                checked={this.state.privateindividual == 'true'}
                                onChange={this.changeHandler} />
                            <label className="form-check-label">Pessoa Fisica</label>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-3 form-group">
                            <label>{this.state.privateindividual == 'false' ? 'CNPJ' : 'CPF'}</label>
                            <input
                                className="form-control"
                                type="text"
                                name="cpfcnpj"
                                value={this.state.cpfcnpj}
                                onChange={this.changeHandler}
                            />
                        </div>
                        <div className="col-3 form-group">
                            <label>Nome</label>
                            <input
                                className="form-control"
                                type="text"
                                name="name"
                                value={this.state.name}
                                onChange={this.changeHandler}
                            />
                        </div>
                    </div>                    
                    <div>
                        {this.state.privateindividual == 'false' ? null :
                            <div>
                                <div className="row">
                                    <div className="col-3 form-group">
                                        <label>RG</label>
                                        <input
                                            className="form-control"
                                            type="text"
                                            name="rg"
                                            value={this.state.rg}
                                            onChange={this.changeHandler}
                                        />
                                    </div>

                                    <div className="col-3 form-group">
                                        <label>Data Nascimento</label>
                                        <DateTimePicker
                                            format="DD/MM/YYYY"
                                            value={this.state.birthdate}
                                            onChange={this.changeBirthDateHandler}
                                            time={false}
                                        />
                                    </div>
                                </div>
                            </div>
                        }
                    </div>
                    <div className="row">
                        <div className="col-3 form-group">
                            <label>Telefone</label>
                            <input
                                className="form-control"
                                type="text"
                                name="newphone"
                                value={this.state.newphone}
                                onChange={this.changeHandler}
                            />
                        </div>
                        <div className="col-1 form-group">                            
                            <input
                                type="checkbox"
                                className="form-check-input"
                                name="newphoneisresidential"
                                style={phoneCheckStyle}
                                onChange={this.changeCheckBoxHandler}
                            />
                            <label className="form-check-label" style={phoneOthersStyle}>Residencial</label>
                        </div>
                        <div className="col-2">
                            <button className="btn btn-success" type="button" style={phoneOthersStyle} onClick={() => this.AddCurrentPhone()}>Adicionar Telefone</button>
                        </div>
                    </div>

                    <div>
                        {(this.state.phones.length == 0) ? null :
                            <div>
                                <br/>
                                <div className="row">
                                    <div className="col-6 form-group">
                                        <table className='table table-striped'>
                                            <thead>
                                                <tr>
                                                    <th>Numero Telefone</th>
                                                    <th>Residencial</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {this.state.phones.map((phone, i) =>
                                                    <tr key={i}>
                                                        <td>{phone.phonenumber}</td>
                                                        <td>{phone.isresidential ? 'Sim' : 'Nao'}</td>
                                                    </tr>
                                                )}
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        }
                    </div>
                    <div className="row" style={buttonStyle}>
                        <div className="col-2 offset-4">
                            <button className="btn btn-primary" type="submit">Criar Fornecedor</button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}
