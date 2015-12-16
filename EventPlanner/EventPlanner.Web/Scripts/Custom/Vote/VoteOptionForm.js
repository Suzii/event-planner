import React from 'react';
import Options from './Options';
import {FormOptionElement} from './FormOptionElement';

export class VoteOptionForm extends React.Component {
    constructor(props) {
        super(props);
        this.onValueSelected = this.onValueSelected.bind(this);
        this.render = this.render.bind(this);
    }

    onValueSelected (value) {
        //console.debug('%s selected for option with %s id of VOTE %s.', value, this.props.optionId, this.props.usersVoteId);
        this.props.onValueSelectedCallback(this.props.optionId, this.props.usersVoteId, value);
    } 

    render() {
        console.log(Options);
        console.log(Options.YES);

        return (
            <div>
                <FormOptionElement option={Options.YES} name={this.props.optionId} onValueSelectedCallback={this.onValueSelected} isSelected={this.props.preSelectedValue == Options.YES} />
                <FormOptionElement option={Options.MAYBE} name={this.props.optionId} onValueSelectedCallback={this.onValueSelected} isSelected={this.props.preSelectedValue == Options.MAYBE} />
                <FormOptionElement option={Options.NO} name={this.props.optionId} onValueSelectedCallback={this.onValueSelected} isSelected={this.props.preSelectedValue == Options.NO} />
            </div>
        );
    }
}

VoteOptionForm.propTypes = {
        optionId: React.PropTypes.string.isRequired,
        usersVoteId: React.PropTypes.string,
        preSelectedValue: React.PropTypes.string,
        onValueSelectedCallback: React.PropTypes.func.isRequired
    };