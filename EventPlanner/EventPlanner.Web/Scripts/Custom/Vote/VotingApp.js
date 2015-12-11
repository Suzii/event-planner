import React from 'react';
import classNames from 'classnames';
import Options from './Options';
import {Spinner} from './Spinner';
import {VoteOption} from './VoteOption';

export class VotingApp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            options: [],
            totalNumberOfVoters: 0,
            loading: true
        };

        this.mapOption = this.mapOption.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.submitVote = this.submitVote.bind(this);
        this.render = this.render.bind(this);
    }

    mapOption(option) {
        return {
            title : option.Title,
            desc: option.Desc,
            optionId: option.Id,
            preSelectedValue: option.UsersVote.WillAttend,
            usersVoteId: option.UsersVote.Id,
            votes: {
                yes: option.Votes.Yes,
                maybe: option.Votes.Maybe,
                no: option.Votes.No
            }
        }
    }

    componentDidMount() {
        $.get(this.props.getInitialDataUrl).success((data)=>{
            console.debug('Initial data for voting app:');
            console.debug(data);
            var totalNumberOfVoters = data.TotalNumberOfVoters;
            var options = data.Options.map(this.mapOption);

            this.setState({
                options: options, 
                totalNumberOfVoters: totalNumberOfVoters,
                loading: false
            });

            $('[data-toggle="popover"]').popover({
                placement: 'top',
                triger: 'click hover'
            });
        });
    }

    submitVote(optionId, usersVoteId, willAttend) {
        console.log('Submitting vote...');
        var data = {
            eventId: this.props.eventId,
            optionId: optionId,
            usersVoteId: usersVoteId,
            willAttend: willAttend
        };
        
        var self = this;
        var successCallback = (data) => {
            console.debug('Vote for data submitted successfully...');
            var option = this.mapOption(data.Option);
            var newOptions = self.state.options;
            var index = newOptions.findIndex((elem, index) => elem.optionId === optionId);
            if(index > -1){
                newOptions[index].votes = option.votes;
                newOptions[index].usersVoteId = option.usersVoteId
            }

            self.setState({
                totalNumberOfVoters: data.TotalNumberOfVoters,
                options: newOptions
            })
        };

        $.ajax({
            type: "POST",
            url: self.props.submitVotesUrl,
            data: data,
            success: successCallback,
            dataType: 'JSON'
        });
    }

    render() {
        if(this.state.loading){
            return (
                <Spinner imgUrl={this.props.loadingImgUrl} />
            )
        } else{
            return (
                 <div>
                    {
                        this.state.options.map((option, index) => {
                            var classes = {
                                'allow-space' : true,
                                'even-row': (index%2 == 0),
                                'odd-row': (index%2 == 1),
                            };
                            return(
                                <div className={classNames(classes)}>
                                    <VoteOption key={option.optionId}
                                         optionId={option.optionId}
                                         title={option.title}
                                         desc={option.desc}
                                         onVoteCallback={this.submitVote}
                                         preSelectedValue={option.preSelectedValue}
                                         usersVoteId={option.usersVoteId}
                                         votes={option.votes}
                                         totalNumberOfVoters={this.state.totalNumberOfVoters} />
                                </div> 
                            )
                        })
                    }
                </div>
            );
        }
    }

}

VotingApp.propTypes = {
        loadingImgUrl: React.PropTypes.string,
        eventId: React.PropTypes.string,
        submitVotesUrl: React.PropTypes.string,
        getInitialDataUrl: React.PropTypes.string
    };

VotingApp.defaultProps = {
        eventId: $('#VotingApp').attr('data-event-id'),
        submitVotesUrl: $('#VotingApp').attr('data-submit-vote-url'),
        getInitialDataUrl: $('#VotingApp').attr('data-get-initial-data-url'),
        loadingImgUrl: $('#VotingApp').attr('data-loading-img-url')
    };