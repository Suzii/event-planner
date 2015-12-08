var Options = {
    YES : 'Yes',
    MAYBE : 'Maybe',
    NO : 'No'
};

var MappingHelper = {
    mapOption: function(option){
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
};

var Tooltip = React.createClass({
    propTypes: {
        data: React.PropTypes.string,
    },
    render: function() {
        return (
            <div />
        );
    }

});

var VotingApp = React.createClass({
    propTypes: {
        loadingImgUrl: React.PropTypes.string,
        eventId: React.PropTypes.string,
        submitVotesUrl: React.PropTypes.string,
        getInitialDataUrl: React.PropTypes.string
    },
    getInitialState: function() {
        return {
            options: [],
            totalNumberOfVoters: 0,
            loading: true
        };
    },
    getDefaultProps: function() {
        var selector = '#VotingApp';
        return {
            eventId: $(selector).attr('data-event-id'),
            submitVotesUrl: $(selector).attr('data-submit-vote-url'),
            getInitialDataUrl: $(selector).attr('data-get-initial-data-url'),
            loadingImgUrl: $(selector).attr('data-loading-img-url')
        };
    },    
    componentDidMount: function() {
        $.get(this.props.getInitialDataUrl).success((data)=>{
            console.debug('Initial data for voting app:');
            console.debug(data);
            var totalNumberOfVoters = data.TotalNumberOfVoters;
            var options = data.Options.map(MappingHelper.mapOption);
            if(this.isMounted()) {
                this.setState({
                    options: options, 
                    totalNumberOfVoters: totalNumberOfVoters,
                    loading: false
                });

                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    triger: 'click hover'
                });
            }
        });
    },
    submitVote: function(optionId, usersVoteId, willAttend){
        console.log('Submitting vote...');
        var data = {
            eventId: this.props.eventId,
            optionId: optionId,
            usersVoteId: usersVoteId,
            willAttend: willAttend
        };
        
        var self = this;
        var successCallback = function(data){
            console.debug('Vote for data submitted successfully...');
            var option = MappingHelper.mapOption(data.Option);
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
    },
    render: function() {
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
                            console.log(classes);
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

});

var VoteOption = React.createClass({
    propTypes: {
        optionId: React.PropTypes.string.isRequired,
        usersVoteId: React.PropTypes.string,
        title: React.PropTypes.string.isRequired,
        desc: React.PropTypes.string.isRequired,
        onVoteCallback: React.PropTypes.func.isRequired,
        preSelectedValue: React.PropTypes.string,
        votes: React.PropTypes.object.isRequired
    },
    getVotersFor: function(voteType) {
        switch (voteType) {
          case Options.YES: return this.props.votes.yes;
          case Options.MAYBE: return this.props.votes.maybe;
          case Options.NO: return this.props.votes.no;
        }
    },
    render: function() {
        return (
            <div className="row">
                <div className="col-sm-2 col-xs-6">
                    <span className="text-primary bold">{this.props.title}</span> <br/>
                    <span className="text-muted">{this.props.desc}</span>
                </div>
            
                <div className="col-sm-2 col-xs-6 center-vertically">
                    <VoteOptionForm 
                        optionId={this.props.optionId} 
                        usersVoteId={this.props.usersVoteId}
                        onValueSelectedCallback={this.props.onVoteCallback} 
                        preSelectedValue={this.props.preSelectedValue} />
                </div>
                <div className="col-sm-8 col-xs-12 center-vertically" style={{marginTop: '0.8em'}}>
                    <Graph 
                        yesVoters={this.getVotersFor(Options.YES)} 
                        maybeVoters={this.getVotersFor(Options.MAYBE)} 
                        noVoters={this.getVotersFor(Options.NO)} 
                        totalNumberOfVoters={this.props.totalNumberOfVoters} />
                </div>
            </div>
        );
    }

});

var FormOptionElement = React.createClass({
    propTypes: {
        option: React.PropTypes.string.isRequired,
        name: React.PropTypes.string.isRequired,
        onValueSelectedCallback: React.PropTypes.func.isRequired,
        isSelected: React.PropTypes.bool,
    },
    getDefaultProps: function() {
        return {
            isSelected: false
        };
    },
    getElementClasses : function() {
      return {
          'glyphicon': true,
          'glyphicon glyphicon-ok yes-option': this.props.option === Options.YES,
          'glyphicon maybe-option': this.props.option === Options.MAYBE,
          'glyphicon glyphicon-remove no-option': this.props.option === Options.NO,
        }
    },
    render: function() {
        return (
            <label>
                <input type="radio" name={this.props.name} value={this.props.option} onChange={()=>this.props.onValueSelectedCallback(this.props.option)} defaultChecked={this.props.isSelected}/>
                <i className={classNames(this.getElementClasses())} title={this.props.option}>{ this.props.option === Options.MAYBE ? "?" : ""}<span className="sr-only">{this.props.option}</span></i>
            </label>
        );
    }

});

var VoteOptionForm = React.createClass({
    propTypes: {
        optionId: React.PropTypes.string.isRequired,
        usersVoteId: React.PropTypes.string,
        preSelectedValue: React.PropTypes.string,
        onValueSelectedCallback: React.PropTypes.func.isRequired
    },
    onValueSelected: function(value){
        console.debug('%s selected for option with %s id of VOTE %s.', value, this.props.optionId, this.props.usersVoteId);
        this.props.onValueSelectedCallback(this.props.optionId, this.props.usersVoteId, value);
    },
    render: function() {
        return (
            <div>
                <FormOptionElement option={Options.YES} name={this.props.optionId} onValueSelectedCallback={this.onValueSelected} isSelected={this.props.preSelectedValue == Options.YES} />
                <FormOptionElement option={Options.MAYBE} name={this.props.optionId} onValueSelectedCallback={this.onValueSelected} isSelected={this.props.preSelectedValue == Options.MAYBE} />
                <FormOptionElement option={Options.NO} name={this.props.optionId} onValueSelectedCallback={this.onValueSelected} isSelected={this.props.preSelectedValue == Options.NO} />
            </div>
        );
    }

});

var ProgressBar = React.createClass({
    propTypes: {
        percentage: React.PropTypes.number.isRequired,
        voters: React.PropTypes.array.isRequired,
        voteType: React.PropTypes.string
    },
    getProgressBarClasses : function() {
      return {
          'progress-bar': true,
          'progress-bar-success': this.props.voteType === Options.YES,
          'progress-bar-gold': this.props.voteType === Options.MAYBE,
          'progress-bar-danger': this.props.voteType === Options.NO,
        }
    },
    getInlineStyles: function() {
        return {width: this.props.percentage + '%'};
    },
    getVotersAsString: function() {
        if(!this.props.voters || !this.props.voters.length) {
            return "";
        }

        return this.props.voters.reduce(((prev, curr) => prev + ', ' + curr));
    },
    getTooltipTitle: function() {
        return this.props.voteType + " voters!";
    },
    render: function() {
        // TODO add proper tooltion to display names of voters 
        return (
            <div className={classNames(this.getProgressBarClasses())} style={this.getInlineStyles()}
                data-toggle="popover" title={this.getTooltipTitle()} data-content={this.getVotersAsString()}>
                <span className="" >{this.props.voters.length}</span>
            </div>
        );
    }
});

var Graph = React.createClass({
    propTypes: {
        yesVoters: React.PropTypes.array.isRequired,
        maybeVoters: React.PropTypes.array.isRequired,
        noVoters: React.PropTypes.array.isRequired,
        totalNumberOfVoters: React.PropTypes.number
    },
    getTotalNumberOfVotes: function() {
        return this.props.totalNumberOfVoters || this.props.yesVoters.length + this.props.maybeVoters.length + this.props.noVoters.length;
    },
    isGraphEmpty : function() {
        return (this.props.yesVoters.length + this.props.maybeVoters.length + this.props.noVoters.length) === 0;
    },
    getBarPercentageFor: function(voteType){
        var x = 0;
        switch (voteType) {
          case Options.YES: x = this.props.yesVoters.length; break;
          case Options.MAYBE: x = this.props.maybeVoters.length; break;
          case Options.NO: x = this.props.noVoters.length; break;
        }

        return Math.round((x*100) / this.getTotalNumberOfVotes(), 0);
    },
    componentWillReceiveProps: function(nextProps) {
        console.log('Vote Graph will recieve new props:');
        console.log(nextProps);
    },
    render: function() {
        if (!this.isGraphEmpty())
        {
            return (
                <div className="progress">
                    <ProgressBar voteType={Options.YES} voters={this.props.yesVoters} percentage={this.getBarPercentageFor(Options.YES)} />
                    <ProgressBar voteType={Options.MAYBE} voters={this.props.maybeVoters} percentage={this.getBarPercentageFor(Options.MAYBE)} />
                    <ProgressBar voteType={Options.NO} voters={this.props.noVoters} percentage={this.getBarPercentageFor(Options.NO)} />
                </div>
            );
        }
        else
        {
            return(
                <div className="progress">
                    <div className="text-center text-success">
                        Be the first one to vote for this option! Total number of voters is {this.props.totalNumberOfVoters}.
                    </div>
                </div>
            );
        }
    }
});