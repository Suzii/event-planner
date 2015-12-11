import React from 'react';
import Options from './Options';
import classNames from 'classnames';

export class FormOptionElement extends React.Component {
    constructor(props) {
        super(props);
        this.getElementClasses = this.getElementClasses.bind(this);
        this.render = this.render.bind(this);

    }

    getElementClasses() {
      return {
          'glyphicon': true,
          'glyphicon glyphicon-ok yes-option': this.props.option === Options.YES,
          'glyphicon maybe-option': this.props.option === Options.MAYBE,
          'glyphicon glyphicon-remove no-option': this.props.option === Options.NO,
        }
    }

    render() {
        return (
            <label>
                <input type="radio" name={this.props.name} value={this.props.option} onChange={()=>this.props.onValueSelectedCallback(this.props.option)} defaultChecked={this.props.isSelected}/>
                <i className={classNames(this.getElementClasses())} title={this.props.option}>{ this.props.option === Options.MAYBE ? "?" : ""}<span className="sr-only">{this.props.option}</span></i>
            </label>
        );
    }
}

FormOptionElement.propTypes = {
        option: React.PropTypes.string.isRequired,
        name: React.PropTypes.string.isRequired,
        onValueSelectedCallback: React.PropTypes.func.isRequired,
        isSelected: React.PropTypes.bool,
    };

FormOptionElement.defaultProps = {
        isSelected: false
    };