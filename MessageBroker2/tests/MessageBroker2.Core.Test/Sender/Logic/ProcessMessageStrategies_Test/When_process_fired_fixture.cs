using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessageBroker2.Core.Sender.Logic;
using MessageBroker2.Core.Sender.Logic.Exceptions;
using MessageBroker2.Core.Sender.Plugins;
using NSubstitute;
using Shouldly;

namespace MessageBroker2.Core.Test.Sender.Logic.ProcessMessageStrategies_Test
{
    public class When_process_fired_fixture : LoggerTestContext
    {
        private IReadDataFor _reader;
        private ISendDataFor _sender;

        public When_process_fired_fixture()
        {
            _reader = Substitute.For<IReadDataFor>();
            _sender = Substitute.For<ISendDataFor>();
        }

        public async Task<IProcessMessageRaport> Act()
        {
            var strategies = CreateStrategies();
            return await strategies.Process();
        }

        private ProcessMessageStrategies CreateStrategies()
        {
            return new ProcessMessageStrategies(GetLogger(), _reader, _sender);
        }

        private void ConfigureReaderToReturnSomeMessages(int readedMessage, int erroMessages)
        {
            _reader.ReadData().ReturnsForAnyArgs(c =>
            {
                List<IDataToSend> dataToSends = new List<IDataToSend>();
                for (int i = 0; i < erroMessages; i++)
                {
                    dataToSends.Add(new ExceptionDataToSendMok());
                }
                for (int i = 0; i < readedMessage - erroMessages; i++)
                {
                    dataToSends.Add(new DataToSendMock());
                }
                return dataToSends;
            });
        }

        private void ConfigureSender()
        {
            _sender.Send(Arg.Any<DataToSendMock>()).Returns(c => Task.CompletedTask);
            _sender.Send(Arg.Any<ExceptionDataToSendMok>()).Returns(c => Task.FromException(new Exception("test")));
        }

        public void Arrange_read_data_from_store()
        {

        }

        public async Task Assert_read_data_from_store(IProcessMessageRaport result)
        {
            await _reader.Received().ReadData();
        }

        public void Arrange_and_no_data_read_end_process_with_NotReadAnyDataToProcess()
        {

        }

        public async Task Assert_and_no_data_read_end_process_with_NotReadAnyDataToProcess(IProcessMessageRaport result)
        {
            await _reader.Received().ReadData();
            result.ShouldBeOfType<NotReadAnyDataToProcess>();
        }


        public void Arrange_and_reader_data_source_not_avaliable_throw_ReaderDataSourceNotAvaliable()
        {
            _reader.WhenForAnyArgs(c => c.ReadData()).Do(c => throw new ReaderDataSourceNotAvaliable("test", new Exception("test inner exception")));
        }

        public void Arrange_send_message_for_all_message_that_was_return(int readDataCount)
        {
            ConfigureReaderToReturnSomeMessages(readDataCount, 0);
            ConfigureSender();

        }



        public Task Assert_send_message_for_all_message_that_was_return(IProcessMessageRaport result, int readDataCount)
        {
            _sender.ReceivedWithAnyArgs(readDataCount).Send(Arg.Any<IDataToSend>());
            return Task.CompletedTask;
        }

        public void Arrange_try_to_send_all_data_even_if_error_occured(int readDataCount, int errorSendsCount)
        {
            ConfigureReaderToReturnSomeMessages(readDataCount, errorSendsCount);
            ConfigureSender();
        }

        public void Assert_try_to_send_all_data_even_if_error_occured(IProcessMessageRaport result, int readDataCount)
        {
            _sender.ReceivedWithAnyArgs(readDataCount).Send(Arg.Any<IDataToSend>());
        }

        public void Arrange_and_data_was_read_and_all_data_succesful_send_report_should_be_correct(int dataReaded)
        {
            ConfigureReaderToReturnSomeMessages(dataReaded, 0);
            ConfigureSender();
        }

        public void Assert_and_data_was_read_and_all_data_succesful_send_report_should_be_correct(IProcessMessageRaport result,int dataReaded)
        {
            result.ReadedMessageCount.ShouldBe(dataReaded);
            result.SendedMessageCount.ShouldBe(dataReaded);
            result.UnsenedMessageCount.ShouldBe(0);
        }

        public void Arranage_and_data_was_read_and_there_was_problem_with_sending_some_data_then_report_should_be_correct(int readedMessageCount, int errorMessageCount)
        {
            ConfigureReaderToReturnSomeMessages(readedMessageCount,errorMessageCount);
            ConfigureSender();
        }

        public void Assert_and_data_was_read_and_there_was_problem_with_sending_some_data_then_report_should_be_correct(IProcessMessageRaport result, int readedMessageCount, int errorMessageCount)
        {
            result.ReadedMessageCount.ShouldBe(readedMessageCount);
            result.SendedMessageCount.ShouldBe(readedMessageCount-errorMessageCount);
            result.UnsenedMessageCount.ShouldBe(errorMessageCount);
        }
    }
}