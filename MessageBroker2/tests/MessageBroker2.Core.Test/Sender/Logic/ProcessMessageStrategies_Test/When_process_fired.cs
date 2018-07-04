using MessageBroker2.Core.Sender.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBroker2.Core.Sender.Logic.Exceptions;
using NSubstitute;
using Serilog;
using Shouldly;
using Xunit;

namespace MessageBroker2.Core.Test.Sender.Logic.ProcessMessageStrategies_Test
{
    public class When_process_fired
    {
        private When_process_fired_fixture _fixture;
        public When_process_fired()
        {
            _fixture = new When_process_fired_fixture();
        }



        [Fact]
        public async Task Read_data_from_store()
        {
            _fixture.Arrange_read_data_from_store();
            var result = await _fixture.Act();
            await _fixture.Assert_read_data_from_store(result); 
        }

        [Fact]
        public async Task And_no_data_read_end_process_with_NotReadAnyDataToProcess()
        {
            _fixture.Arrange_and_no_data_read_end_process_with_NotReadAnyDataToProcess();
            var result = await _fixture.Act();
            await _fixture.Assert_and_no_data_read_end_process_with_NotReadAnyDataToProcess(result);
        }

        [Fact]
        public async Task And_reader_data_source_not_avaliable_throw_ReaderDataSourceNotAvaliable()
        {
            _fixture.Arrange_and_reader_data_source_not_avaliable_throw_ReaderDataSourceNotAvaliable();
            ShouldThrowTaskExtensions.ShouldThrow<ReaderDataSourceNotAvaliable>(() => _fixture.Act());
        }

        [Fact]
        public async Task Send_message_for_all_message_that_was_return()
        {
            _fixture.Arrange_send_message_for_all_message_that_was_return(10);
            var result=await _fixture.Act();
            await _fixture.Assert_send_message_for_all_message_that_was_return(result,10);
        }

        [Fact]
        public async Task Try_to_send_all_data_even_if_error_occured()
        {
            _fixture.Arrange_try_to_send_all_data_even_if_error_occured(10, 2);
            var result = await _fixture.Act();
            _fixture.Assert_try_to_send_all_data_even_if_error_occured(result, 10);

        }

        [Fact]
        public async Task And_data_was_read_and_all_data_succesful_send_report_should_be_correct()
        {
            _fixture.Arrange_and_data_was_read_and_all_data_succesful_send_report_should_be_correct(10);
            var result = await _fixture.Act();
            _fixture.Assert_and_data_was_read_and_all_data_succesful_send_report_should_be_correct(result,10);
        }

        [Fact]
        public async Task And_data_was_read_and_there_was_problem_with_sending_some_data_then_report_should_be_correct()
        {
            _fixture
                .Arranage_and_data_was_read_and_there_was_problem_with_sending_some_data_then_report_should_be_correct(10, 2);
            var result = await _fixture.Act();
            _fixture
                .Assert_and_data_was_read_and_there_was_problem_with_sending_some_data_then_report_should_be_correct(
                    result, 10, 2);
        }
    }
}
